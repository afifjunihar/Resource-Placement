using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.ViewModels
{
	public class LoginDataVM
	{
		public string EmailOrUsername { get; set; }
		public string[] Roles { get; set; }
	}
}
