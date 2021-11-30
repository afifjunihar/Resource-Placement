using API.Models;
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
    public class ProjectsController  : BaseController<Project, ProjectRepository, int>
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

        public IActionResult Details(string Id)
        {
            ViewData["encodeID"] = Id;
            return View();
        }

        public async Task<JsonResult> GetDetails(string Id)
        {
            var result = await project.Details(Id);
            return Json(result);
        }

        public async Task<JsonResult> Handler(string Id)
        {
            var result = await project.Handler(Id);
            return Json(result);
        }

    }
}
