﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.ViewModels
{
	public class LoginVM
	{
		public string EmailOrUsername { get; set; }
		public string Password { get; set; }
	}
}