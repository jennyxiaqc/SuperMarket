using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xy.SuperMarket.Domain.Entities;

namespace Xy.SuperMarket.WebApp.Models
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}