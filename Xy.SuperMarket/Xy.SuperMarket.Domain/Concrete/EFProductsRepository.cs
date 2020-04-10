using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xy.SuperMarket.Domain.Abstract;
using Xy.SuperMarket.Domain.Entities;

namespace Xy.SuperMarket.Domain.Concrete
{
    public class EFProductsRepository: IProductsRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Product> Products 
        {
            get { return context.Products; }
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductId==0)
            {
                context.Products.Add(product);
            }
            else
            {
                Product dbEntry = context.Products
                    .Where(x => x.ProductId == product.ProductId)
                    .FirstOrDefault();
                if (dbEntry!=null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Catergory = product.Catergory;
                    dbEntry.Description = product.Description;
                    dbEntry.Unit = product.Unit;
                    dbEntry.Price = product.Price;
                }
            }
            context.SaveChanges();
        }

        public Product DeleteProduct(int productId)
        {
            Product dbEntry = context.Products.Find(productId);

            if (dbEntry!=null)
            { 
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;

        }
    }
}
