using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using Xy.SuperMarket.Domain.Abstract;
using Xy.SuperMarket.Domain.Entities;
using Xy.SuperMarket.WebApp.Concrete;
using Xy.SuperMarket.WebApp.Models;

namespace Xy.SuperMarket.WebApp.Controllers
{
    public class CartController : Controller
    {
        private IProductsRepository productRepository;
        // private IOrderProcessor orderProcessor;
        //   public CartController(IProductsRepository repo, IOrderProcessor processor)
        public CartController(IProductsRepository repo)
        {
            productRepository = repo;
          //  orderProcessor = processor;
        }

        // public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        public ActionResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            Product product = productRepository.Products
                .Where(p => p.ProductId == productId)
                .FirstOrDefault();
            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            //return RedirectToAction("Index", new { returnUrl });
            //return Redirect(Request.Url.Scheme + "://" + Request.Url.Authority + returnUrl);
            return Redirect(returnUrl);
            //return View();

        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            Product product = productRepository.Products
                .Where(p => p.ProductId == productId)
                .FirstOrDefault();
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        // GET: Cart
        public ActionResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public RedirectToRouteResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                Session["cart"] = cart;
                Session["shippingDetails"] = shippingDetails;
                return RedirectToAction("PaymentWithPaypal", "PaypalPayment");

                //orderProcessor.ProcessOrder(cart, shippingDetails);
                //cart.clear();
                //return View("Completed");
            }
            else
            {
                return RedirectToAction("Checkout",shippingDetails);
            }
        }

		//*********
		
	}
}