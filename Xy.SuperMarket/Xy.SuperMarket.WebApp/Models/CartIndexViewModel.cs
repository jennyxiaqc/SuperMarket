using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xy.SuperMarket.Domain.Entities;

namespace Xy.SuperMarket.WebApp.Models
{
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}