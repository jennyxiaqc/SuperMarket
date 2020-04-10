using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xy.SuperMarket.Domain.Abstract;

namespace Xy.SuperMarket.WebApp.Controllers
{
    public class AdminController : Controller
    {
       private IProductsRepository repository;
        public AdminController(IProductsRepository repo)
        {
            repository = repo;
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View(repository.Products);
        }

        public ActionResult Edit(int productId)
        {
            var result = repository.Products
                .Where(x => x.ProductId == productId)
                .FirstOrDefault();
            return View(result);
        }
    }
}