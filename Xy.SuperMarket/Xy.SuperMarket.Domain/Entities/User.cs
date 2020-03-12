using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xy.SuperMarket.Domain.Entities
{

    public class User
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public Enums.Language Language { get; set; }
        public Enums.PaymentMethod  PaymentMethod { get; set; }
        public string EmailAddress { get; set; }
        public Address HomeAddress { get; set; }
        public Address MailAddress { get; set; }
        public Enums.UserGroup Group { get ; set ; }



    }
}
