﻿using API.Models;
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

		public async Task<JsonResult> Current(string id)
		{
			var result = await interview.Current(id);
			return Json(result);
		}

		public async Task<JsonResult> History(string id)
		{
			var result = await interview.History(id);
			return Json(result);
		}
	}
}

