using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xy.SuperMarket.Domain.Abstract;
using Xy.SuperMarket.Domain.Entities;

namespace Xy.SuperMarket.Domain.Concrete
{
    public class EFUsersRepository:IUsersRepository
    {
        private EFDbContext context = new EFDbContext();
        public IEnumerable<User> Users { get { return context.Users; } }
    }
}

