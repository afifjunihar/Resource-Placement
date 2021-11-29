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
    public class ProjectsController  : BaseController<KeyVM, ProjectRepository, string>
    {

        private readonly ProjectRepository project;
        public ProjectsController(ProjectRepository repository) : base(repository)
        {
            this.project = repository;
        }

        public JsonResult ListProjectApplicant(KeyVM entity)
        {
            var result = project.GetProjectApplicant(entity);
            return Json(result);
        }
        public async Task<JsonResult> ListProject()
        {
            var result = await project.Current();
            return Json(result);
        }
        public async Task<JsonResult> ListOpenProject()
        {
            var result = await project.openProject();
            return Json(result);
        }

        public async Task<JsonResult> ListClosedProject()
        {
            var result = await project.closedProject();
            return Json(result);
        }
    }
}
