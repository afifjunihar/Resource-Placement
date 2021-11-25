using API.Models;
using Client.Base;
using Client.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
	public class InterviewsController : BaseController<Interview, InterviewRepository, int>
	{
		private readonly InterviewRepository interview;

		public InterviewsController(InterviewRepository repository) : base(repository)
		{
			this.interview = repository;
		}
		public IActionResult Index()
		{
			return View();
		}
	}
}

