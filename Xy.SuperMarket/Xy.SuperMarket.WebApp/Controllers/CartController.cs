using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xy.SuperMarket.Domain.Abstract;
using Xy.SuperMarket.Domain.Entities;
using Xy.SuperMarket.WebApp.Models;

namespace Xy.SuperMarket.WebApp.Controllers
{
    public class CartController : Controller
    {
        private IProductsRepository productRepository;
        private IOrderProcessor orderProcessor;
        public CartController(IProductsRepository repo, IOrderProcessor processor)
        {
            productRepository = repo;
            orderProcessor = processor;
        }

        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            Product product = productRepository.Products
                .Where(p => p.ProductId == productId)
                .FirstOrDefault();
            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
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
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }
    }
}