using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Xy.SuperMarket.Domain.Concrete;
using Xy.SuperMarket.WebApp.Abstract;

namespace Xy.SuperMarket.WebApp.Concrete
{
	public class EFAuthProvider : IAuthProvider
	{
		public EFDbContext Context { get; set; } 
		public bool Authenticate(string username, string password)
		{
			bool result = false;
			var user = Context.AdminUsers
				.Where(x => x.UserName == username && x.Password == password)
				.FirstOrDefault();
			if (user!=null)
			{ 
				result = true;
				FormsAuthentication.SetAuthCookie(username, false);
			}
			return result;

		}
	}
}