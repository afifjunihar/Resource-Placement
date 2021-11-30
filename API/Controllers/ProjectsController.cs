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
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : BaseController<Project, ProjectRepository, int>
    {
        public readonly ProjectRepository project;
        public IConfiguration _configuration;
        public ProjectsController(ProjectRepository projectRepository, IConfiguration configuration) : base(projectRepository)
        {
            this.project = projectRepository;
            this._configuration = configuration;
        }

        [HttpPost]
        [Route("ProjectApplicant")]
        public ActionResult showApplicant(KeyVM keyVM) 
        {
            var result = project.showApplicant(keyVM);
            return Ok(result);
        }

        [HttpGet]
        [Route("Applicant")]
        public ActionResult Applicant(KeyVM keyVM)
        {
            var result = project.projectInterview(keyVM);
            return Ok(result);
        }

        [HttpGet]
        [Route("OpenProject")]
        public ActionResult OpenProject()
        {
            var result = project.openProject();
            return Ok(result);
        }
        [HttpGet]
        [Route("ClosedProject")]
        public ActionResult ClosedProject()
        {
            var result = project.closedProject();
            return Ok(result);
        }

        [HttpGet]
        [Route("Details/{Key}")]
        public ActionResult Details(string Key)
        {
            Object result = project.Details(Key);
            return Ok(result);
        }

        [HttpGet]
        [Route("Handler/{Id}")]
        public ActionResult Handler(string Id)
        {
            var result = project.Handler(Id);
            if (result != null)
            {
                return Ok(result);
            }
            return Ok(HttpStatusCode.NoContent);
        }

    }
}
