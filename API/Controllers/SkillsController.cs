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
    public class SkillsController : BaseController<Skill, SkillRepository, int>
    {

        public readonly SkillRepository skill;
        public IConfiguration _configuration;
        public SkillsController(SkillRepository skillRepository, IConfiguration configuration) : base(skillRepository)
        {
            this.skill = skillRepository;
            this._configuration = configuration;
        }
    }

}
