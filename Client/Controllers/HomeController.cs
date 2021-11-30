using Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
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
			return View();
		}
		public IActionResult notfound()
		{
			return View();
		}
		public IActionResult unauthorized()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult candidateDashboard()
		{
			return View();
		}

		public IActionResult Login()
		{
			return View();
		}


		public IActionResult addCandidate()
		{
			return View();
		}

		public IActionResult addTrainer()
		{
			return View();
		}
		public IActionResult addClient()
		{
			return View();
		}
		public IActionResult ListProject()
		{
			return View();
		}

		public IActionResult openProject()
		{
			return View();
		}
		public IActionResult closedProject()
		{
			return View();
		}
		public IActionResult gradingCandidate()
		{
			return View();
		}

		public IActionResult aspekPenilaian()
		{
			return View();
		}
		public IActionResult clientDash()
		{
			return View();
		}

		public IActionResult candidateDash()
		{
			return View();
		}

		//[Authorize]
		[Route("/dashboard")]
		public IActionResult Dashboard()
		{
			string role = User.FindFirstValue(ClaimTypes.Role);
			if (role == "Employee")
			{
				return View("~/Views/Home/candidateDash.cshtml");
			}
			else if (role == "Manager") 
			{
				return View("~/Views/Home/addCandidate.cshtml");
			}
			else if (role == "Trainer")
			{
				return View("~/Views/Home/gradingCandidate.cshtml");
			}

			// Implementasi Kode afif...
			return View("~/Views/Home/ClientDash.cshtml");
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
