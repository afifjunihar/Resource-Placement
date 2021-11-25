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
    public class SkillsController  : BaseController<AddSkillVM, SkillRepository, string>
    {

        private readonly SkillRepository skill;
        public SkillsController(SkillRepository repository) : base(repository)
        {
            this.skill = repository;
        }

        public JsonResult GetListSkill()
        {
            var result = skill.GetListSkill();
            return Json(result);
        }

        public JsonResult AddSkill(AddSkillVM entity)
        {
            var result = skill.AddSkill(entity);
            return Json(result);
        }
        public JsonResult UpdateSkill(AddSkillVM entity)
        {
            var result = skill.UpdateSkill(entity);
            return Json(result);
        }
    }
}
