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
    }
}
