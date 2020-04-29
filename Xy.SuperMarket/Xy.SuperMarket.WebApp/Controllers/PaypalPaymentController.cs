using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xy.SuperMarket.Domain.Abstract;
using Xy.SuperMarket.Domain.Entities;
using Xy.SuperMarket.WebApp.Concrete;

namespace Xy.SuperMarket.WebApp.Controllers
{
	public class PaypalPaymentController : Controller
	{
		// GET: Payment

		//public ActionResult Index()
		//{
		//	return View();
		//} 
		private IOrderProcessor orderProcessor;
		public PaypalPaymentController(IOrderProcessor processor)
		{
				  orderProcessor = processor;
		}

		public ActionResult PaymentWithPaypal(Cart cart,ShippingDetails shippingDetails, string Cancel = null)
		{
			////getting the apiContext
			APIContext apiContext = PaypalConfiguration.GetAPIContext();
			try
			{
				//A resource representing a payer that funds a payment method as paypal
				//Payer Id will be returned when payment proceeds or click to pay
				string payerId = Request.Params["PayerId"];
				if (string.IsNullOrEmpty(payerId))
				{
					//this section will be executed first becuse PayerId doesn't exist
					//is is returned by the create function call of the payment class
					//create a payement
					//baseURL is the rul on which paypal sends back the data.
					string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority +
						"/PaypalPayment/PaymentWithPaypal?";
					//here we generate guid for storing the paymentID received in session
					//which will be used in the payment excution
					var guid = Convert.ToString((new Random()).Next(100000000));
					//CreatePayment function give us the payment approval url
					//on which payer is redirected for paypal account payment
					Payment createPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid, cart, shippingDetails);
					//get links returned from paypal in response to Create function call
					var links = createPayment.links.GetEnumerator();
					string paypalRedirectUrl = null;
					while (links.MoveNext())
					{
						Links lnk = links.Current;
						if (lnk.rel.ToLower().Trim().Equals("approval_url"))
						{
							//save the paypalredirect URL to which user will be redirected for payment
							paypalRedirectUrl = lnk.href;
						}
					}
					//saving the paymentID in the key guid
					Session.Add(guid, createPayment.id);
					return Redirect(paypalRedirectUrl);
				}
				else
				{
					//This function executes after receving all parameteres for the payment
					var guid = Request.Params["guid"];
					Payment executedPayment = ExecutePayment(apiContext, payerId,
						Session[guid] as string);
						//Session[guid] as string);
					//if executed payment failed then we will show payment failure message to user
					if (executedPayment.state.ToLower() != "approved")
					{
						return View("PaypalFail");
					}
				}
			}
			catch (Exception ex)
			{
				Session["ex"] = ex.Message;
				return View("PaypalFail");

			}
			// on successful payment,email order to user, show success page to user. 
			orderProcessor.ProcessOrder(cart, shippingDetails);//email order 
			cart.clear();
			return View("PaypalSuccess");
		}
		private PayPal.Api.Payment payment;
		private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
		{
			var paymentExecution = new PaymentExecution()
			{
				payer_id = payerId,
			};
			this.payment = new Payment()
			{
				id = paymentId
			};
			return this.payment.Execute(apiContext, paymentExecution);
		}

		private Payment CreatePayment(APIContext apiContext, string redicrectUrl, Cart cart, ShippingDetails shippingDetails)
		{
			//create itemlisst and add item objects to it
			var itemList = new ItemList()
			{
				items = new List<Item>()
			};
			decimal subTotal = 0;
			foreach (var item in cart.Lines)
			{
				subTotal += item.Product.Price * item.Quantity;
				itemList.items.Add(new Item()
				{
					name = item.Product.Name.ToString(),
					currency = "USD",
					price = item.Product.Price.ToString(),
					quantity = item.Quantity.ToString(),
					sku = "sku",
				});
			}
			var payer = new Payer()
			{
				payment_method = "paypal",
			};
			//configure Redirect Urls here with RedirectUrls object
			var redirUrls = new RedirectUrls()
			{
				cancel_url = redicrectUrl + "&Cancel=true",
				return_url = redicrectUrl
			};
			//adding tax, shipping and Subtotal details
			var details = new Details()
			{
				tax = "1",
				shipping = "1",
				subtotal = subTotal.ToString()
			};
			//final amount with details
			var amount = new Amount()
			{
				currency = "USD",
				total = (subTotal + 2).ToString(),
				details = details
			};
			var transactionList = new List<Transaction>();
			//Adding description about the transaction
			transactionList.Add(new Transaction()
			{
				description = "Transaction description:Super Store",
				invoice_number = "10020",//your generated invoice numner
				amount = amount,
				item_list = itemList
			});
			this.payment = new Payment()
			{
				intent = "sale",
				payer = payer,
				transactions = transactionList,
				redirect_urls = redirUrls
			};
			//return this.payment.Create(apiContext);
			return Payment.Create(apiContext, payment);
		}
	}
}