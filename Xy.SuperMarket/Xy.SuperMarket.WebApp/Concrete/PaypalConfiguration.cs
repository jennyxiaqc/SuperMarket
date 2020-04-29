using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Xy.SuperMarket.WebApp.Concrete
{
	public class PaypalConfiguration
	{
		//varialbles for storing the cliendtID and clientSecret key
		public readonly static string ClientId;
		public readonly static string ClientScrect;
		//constructor
		static PaypalConfiguration()
		{
			var config = GetConfig();
			ClientId = config["clientId"];
			ClientScrect = config["clientSecret"];
		}
		//getting properties from the web.config
		public static Dictionary<string,string> GetConfig()
		{
			return PayPal.Api.ConfigManager.Instance.GetProperties();
		}
		private static string GetAccessToken()
		{
			//get accesstoken from paypal
			string accessToken = new OAuthTokenCredential(ClientId, ClientScrect, GetConfig())
				.GetAccessToken();
			return accessToken;
		}

		public static APIContext GetAPIContext()
		{
			//return apicontext pbject by invoking it with the accesstoken
			APIContext apiContext = new APIContext(GetAccessToken());
			apiContext.Config = GetConfig();
			return apiContext;
		}
	}
}