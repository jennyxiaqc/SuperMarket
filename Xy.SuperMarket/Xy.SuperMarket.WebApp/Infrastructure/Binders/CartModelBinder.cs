using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Xy.SuperMarket.Domain.Entities;

namespace Xy.SuperMarket.WebApp.Infrastructure.Binders
{
    public class CartModelBinder : IModelBinder
    {
        private string sessionKey = "Cart";
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Cart cart = null;
            if (controllerContext.HttpContext.Session!=null)
            {
                cart = (Cart)controllerContext.HttpContext.Session[sessionKey];
            };
            if (cart==null)
            {
                cart = new Cart();
                if (controllerContext.HttpContext.Session!=null)
                { 
                    controllerContext.HttpContext.Session[sessionKey] = cart;
                }
            }
            return cart;
        }
    }
}