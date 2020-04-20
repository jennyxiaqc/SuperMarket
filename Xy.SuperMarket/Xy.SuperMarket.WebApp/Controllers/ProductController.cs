using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xy.SuperMarket.Domain.Abstract;
using Xy.SuperMarket.Domain.Concrete;
using Xy.SuperMarket.Domain.Entities;
using Xy.SuperMarket.WebApp.Models;

namespace Xy.SuperMarket.WebApp.Controllers
{
    public class ProductController : Controller
    {
        //private IProductsRepository repository =new InMemoryProductsRepository();//old way without Autofac
        //public ViewResult List()
        //{
        //    return View(repository.Products);
        //}
        public int PageSize=2;
        public IProductsRepository ProductsRepository { get; set; } //property inject
        // GET: Products,page links
        public ActionResult List(string category,int page=1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                
                Products = ProductsRepository
                .Products
                .OrderBy(p => p.ProductId)
                .Where(p=>category==null||p.Catergory==category)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo=new PagingInfo
                {
                    CurrentPage=page,
                    ItemsPerPage=PageSize,
                    TotalItems= ProductsRepository.Products.Where(p=>category==null||p.Catergory==category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }

        public FileContentResult GetImage(int productId = 0)
        {
            Product product = ProductsRepository.Products
                .Where(x => x.ProductId == productId)
                .FirstOrDefault();
            if (product != null && product.ImageData!=null && product.ImageMimeType!=null)
            {
                return File(product.ImageData, product.ImageMimeType);
            }
            else
            {
                return null;
            }

        }
    }
}