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
    public class RegistersController : BaseController<RegisterVM, RegisterRepository, string>
    {
        private readonly RegisterRepository register;
        public RegistersController(RegisterRepository repository) : base(repository)
        {
            this.register = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult RegisterEmployee(RegisterVM entity)
        {
            var result = register.RegisterCandidate(entity);
            return Json(result);
        }

        public JsonResult RegisterTrainer(RegisterVM entity)
        {
            var result = register.RegisterTrainer(entity);
            return Json(result);
        }

        public JsonResult RegisterClient(RegisterVM entity)
        {
            var result = register.RegisterClient(entity);
            return Json(result);
        }
    }
}
