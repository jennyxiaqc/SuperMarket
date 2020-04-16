using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xy.SuperMarket.Domain.Abstract;
using Xy.SuperMarket.Domain.Entities;

namespace Xy.SuperMarket.WebApp.Controllers
{
    [Authorize]
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

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image!=null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }
                repository.SaveProduct(product);
                TempData["message"] = string.Format("{0} has been saved!",product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                //There is something wrong with the input
                return View(product);
            }
        }

        public ActionResult Create()
        {
            return View("Edit", new Product());
        }

        [HttpPost]
        public ActionResult Delete(int productId=0)
        {
            var deletedProduct = repository.DeleteProduct(productId);
            if (deletedProduct!=null)
            {
                TempData["message"] = string.Format("{0} product is deleted!",
                    deletedProduct.Name);
            }
            return RedirectToAction("Index");
        }

        
    }
}