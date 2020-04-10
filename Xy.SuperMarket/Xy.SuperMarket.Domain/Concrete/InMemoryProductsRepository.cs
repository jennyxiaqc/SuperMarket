using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xy.SuperMarket.Domain.Abstract;
using Xy.SuperMarket.Domain.Entities;

namespace Xy.SuperMarket.Domain.Concrete
{
    public class InMemoryProductsRepository:IProductsRepository
    {
        private List<Product> _products = new List<Product>
        {
            new Product{Name="Potato",Catergory="Vegitable",Price=6.99m,  Unit="10Lb/bag"},
            new Product{Name="Tomato",Catergory="vegitable",Price=2.99m, Unit="Lb"},
            new Product{Name="NestCape Cofee",Catergory="Grocery",Price=8.99m, Unit="500g"}
        };

        public IEnumerable<Product> Products 
        { get { return _products; } }

        public Product DeleteProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public void SaveProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
