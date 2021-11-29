using Client.Base;
using Client.Repository.Data;
using Client.Views.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class ProfilesController : BaseController<GetProfileVM, ProfileRepository, string>
    {
        private readonly ProfileRepository profile;
        public ProfilesController(ProfileRepository repository) : base(repository)
        {
            this.profile = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetProfileClient()
        {
            var result = await profile.GetProfileClient();
            return Json(result);
        }

        public async Task<JsonResult> GetProfileCandidate()
        {
            var result = await profile.GetProfileCandidate();
            return Json(result);
        }

        public async Task<JsonResult> GetProfileTrainer()
        {
            var result = await profile.GetProfileTrainer();
            return Json(result);
        }

        public JsonResult GetProfile(string Id)
        {
            var result = profile.GetProfileUser(Id);
            return Json(result);
        }

    }
}
