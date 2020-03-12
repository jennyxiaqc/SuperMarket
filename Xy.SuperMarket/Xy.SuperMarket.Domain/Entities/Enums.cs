using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xy.SuperMarket.Domain.Entities
{
    public partial class Enums
    {
        public enum UserGroup { SA, RegisteredCustomer, Guest }
        public enum PaymentMethod { Visa, MasterCard, Paypal }
        public enum Language { English, Français }
    }
}
