using Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			ViewData["Token"] = HttpContext.Session.GetString("JWToken");
			return View();
		}

		[Authorize]
		[Route("/dashboard")]
		public IActionResult Dashboard()
		{
			string role = User.FindFirstValue(ClaimTypes.Role);
			if (role == "Candidate")
			{
				return View("~/Views/Home/CandidateDash.cshtml");
			}
			// Implementasi Kode afif...
			return View("~/Views/Home/ClientDash.cshtml");
		}

		[Authorize]
		[Route("/logout")]
		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("index");
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
