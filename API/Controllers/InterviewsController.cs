using API.Base;
using API.Models;
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
    }
}
