using API.Models.ViewModels;
using Client.Base;
using Client.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class AssignProjectController : BaseController<InterviewVM, AssignProjectRepository, string>
    {

        private readonly AssignProjectRepository aProject;
        public AssignProjectController(AssignProjectRepository repository) : base(repository)
        {
            this.aProject = repository;
        }
        public JsonResult Assign(InterviewVM entity)
        {
            var result = aProject.AssignProject(entity);
            return Json(result);
        }
    }
}
