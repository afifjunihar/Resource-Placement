using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.ViewModels
{
	public class LoginDataVM
	{
		public string UserId { get; set; }
		public string Fullname { get; set; }
		public string[] Roles { get; set; }
	}
}
