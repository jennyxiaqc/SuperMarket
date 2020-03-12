using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xy.SuperMarket.Domain.Abstract;
using Xy.SuperMarket.Domain.Concrete;

namespace Xy.SuperMarket.WebApp.Controllers
{
    public class ProductController : Controller
    {
        //private IProductsRepository repository =new InMemoryProductsRepository();//old way without Autofac
        //public ViewResult List()
        //{
        //    return View(repository.Products);
        //}

        public IProductsRepository ProductsRepository { get; set; } //property inject
        // GET: Product
        public ActionResult List()
        {
            return View(ProductsRepository.Products);
        }


    }
}