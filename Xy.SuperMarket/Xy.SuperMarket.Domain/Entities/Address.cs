using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xy.SuperMarket.Domain.Entities
{
    public class Address
    {
        public string Province { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string AptNumber { get; set; }
    }
}
