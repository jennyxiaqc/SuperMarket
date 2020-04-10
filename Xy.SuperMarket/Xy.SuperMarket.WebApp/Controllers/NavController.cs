using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xy.SuperMarket.Domain.Abstract;

namespace Xy.SuperMarket.WebApp.Controllers
{
    public class NavController : Controller
    {
        private IProductsRepository productsRepository;
        public NavController(IProductsRepository repo)
        {
            productsRepository = repo;
        }
        // GET: Nav
        public PartialViewResult Menu(string category)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = productsRepository
                .Products
                .Select(x => x.Catergory)
                .Distinct()
                .OrderBy(x => x);
            return PartialView("FlexMenu",categories);
        }
    }
}