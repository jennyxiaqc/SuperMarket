using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xy.SuperMarket.Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Product product,int quantity)
        {
            CartLine cartLine = lineCollection
                .Where(p => p.Product.ProductId == product.ProductId)
                .FirstOrDefault();

            if (cartLine==null)
            {
                lineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                cartLine.Quantity += quantity;
            }
        }

        public void RemoveLine(Product product)
        {
           // lineCollection.RemoveAll(l => l.Product.ProductId == product.ProductId);
            lineCollection.Remove(
                lineCollection
                .Where(l=>l.Product.ProductId==product.ProductId)
                .FirstOrDefault());
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(l => l.Product.Price * l.Quantity);
        }

        public void clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        { 
            get { return lineCollection; } 
        }
    }

    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
