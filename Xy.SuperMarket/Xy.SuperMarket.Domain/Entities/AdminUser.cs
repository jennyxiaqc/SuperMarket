using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xy.SuperMarket.Domain.Entities
{
	public class AdminUser
	{
		[Key]
		public int AdminUserId { get; set; }
		[Required]
		public string UserName { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
