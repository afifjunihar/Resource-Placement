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
    public class LupaPasswordController : BaseController<KeyVM, LupaPasswordRepository, string>
    {
        private readonly LupaPasswordRepository lupaPassword;
        public LupaPasswordController(LupaPasswordRepository repository) : base(repository)
        {
            this.lupaPassword = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult LupaPassword(KeyVM entity)
        {
            var result = lupaPassword.LupaPassword(entity);
            return Json(result);
        }
    }
}
