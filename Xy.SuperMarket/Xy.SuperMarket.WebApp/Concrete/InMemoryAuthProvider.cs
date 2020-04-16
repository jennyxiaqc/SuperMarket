using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Xy.SuperMarket.WebApp.Abstract;

namespace Xy.SuperMarket.WebApp.Concrete
{
	public class InMemoryAuthProvider : IAuthProvider
	{
		public bool Authenticate(string username, string password)
		{
			bool result = false;
			result= (username == "admin1" && password == "pwd1");
			if (result)
			{
				FormsAuthentication.SetAuthCookie(username, false);
			}
			return result;
		}
	}
}