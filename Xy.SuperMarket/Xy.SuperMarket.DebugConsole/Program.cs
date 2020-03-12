using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xy.SuperMarket.Domain.Concrete;
using Xy.SuperMarket.Domain.Entities;

namespace Xy.SuperMarket.DebugConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = new EFDbContext())
            //{
            //    var product = new Product { Name = "Potato", Catergory = "Vegitable",Description="product of Quebec", Price = 6.99m, Unit = "10Lb/bag" };
            //    ctx.Products.Add(product);
            //    product = new Product { Name = "Tomato", Catergory = "vegitable", Description = "product of USA",Price = 2.99m, Unit = "Lb" };
            //    ctx.Products.Add(product);
            //    product = new Product { Name = "NestCape Cofee", Catergory = "Grocery", Description = "product of Mexico", Price = 8.99m, Unit = "500g" };
            //    ctx.Products.Add(product);
            //    ctx.SaveChanges();
            //}
            {
                var productToRemove =
                    ctx
                    .Products
                    .ToList()
                    .Take(3)
                    .ToList();

                foreach (var item in productToRemove)
                {
                    ctx.Products.Remove(item);
                }
                ctx.SaveChanges();

                foreach (var product in ctx.Products)
                {
                    Console.WriteLine(product.Name);
                }
            }


            Console.WriteLine("done");
            Console.ReadLine();
            
        }
    }
}
