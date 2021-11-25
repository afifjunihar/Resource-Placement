using API.Models;
using API.Models.ViewModels;
using Client.Base;
using Client.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Client.Controllers
{
	public class AccountsController : BaseController<Account, AccountRepository, int>
	{
		private readonly AccountRepository account;

		public AccountsController(AccountRepository repository) : base(repository)
		{
			this.account = repository;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		[Route("/Login")]
		public async Task<IActionResult> Auth(LoginVM login)
		{
			var jwtToken = await account.Auth(login);
			var token = jwtToken.Token;

			if (token == null)
			{
				if (jwtToken.Message.StartsWith("Password"))
				{
					return Json(new { Status = HttpStatusCode.BadRequest, Message = jwtToken.Message });
				}
				return Json(new { Status = HttpStatusCode.NotFound , Message = jwtToken.Message } );
			}


			HttpContext.Session.SetString("JWToken", token);
			//HttpContext.Session.SetString("Name", jwtHandler.GetName(token));
			//HttpContext.Session.SetString("ProfilePicture", "assets/img/theme/user.png");

			return Json(HttpStatusCode.OK);
		}

		//[HttpPost]
		//[Route("/Login")]
		//public async Task<IActionResult> Auth(LoginVM login)
		//{
		//	var jwtToken = await account.Auth(login);
		//	var token = jwtToken.Token;

		//	if (token == null)
		//	{
		//		return BadRequest(jwtToken.Message);
		//	}

		//	HttpContext.Session.SetString("JWToken", token);
		//	//HttpContext.Session.SetString("Name", jwtHandler.GetName(token));
		//	//HttpContext.Session.SetString("ProfilePicture", "assets/img/theme/user.png");

		//	return Ok(jwtToken.Message);
		//}
	}
}
