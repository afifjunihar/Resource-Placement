using API.Base;
using API.Models;
using API.Models.ViewModels;
using API.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class InterviewsController : BaseController<Interview, InterviewRepository, int>
	{
		public readonly InterviewRepository interview;
		public IConfiguration _configuration;
		public InterviewsController(InterviewRepository interviewRepository, IConfiguration configuration) : base(interviewRepository)
		{
			this.interview = interviewRepository;
			this._configuration = configuration;
		}

		[HttpPost]
		[Route("Assign")]
		public ActionResult AssignInterview(InterviewVM interviewVM)
		{
			var result = interview.AssignInterview(interviewVM);
			return Ok(result);
		}

		[HttpPost]
		[Route("Accept")]
		public ActionResult Accept(KeyVM key)
		{
			var result = interview.AcceptedInterview(key);
			if (result == 0)
			{
				return Ok("Candidates successfully recruited");
			}
			return BadRequest();
		}

		[HttpPost]
		[Route("Reject")]
		public ActionResult Reject(KeyVM key)
		{
			var result = interview.RejectInterview(key);
			if (result == 0)
			{
				return Ok("Candidates successfully rejected");
			}
			return BadRequest();
		}

		[HttpGet]
		[Route("History/{Id}")]
		public ActionResult History(string Id)
		{
			IEnumerable<object> result = interview.History(Id);
			return Ok(result);
		}

		[HttpGet]
		[Route("Current/{Id}")]
		public ActionResult Current(string Id)
		{
			var result = interview.Current(Id);
			return Ok(result);
		}
	}
}
