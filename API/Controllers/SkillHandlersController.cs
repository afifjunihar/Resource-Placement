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
    public class SkillHandlersController : BaseController<SkillHandler, SkillHandlerRepository, int>
    {
        public readonly SkillHandlerRepository skillHandler;
        public IConfiguration _configuration;
        public SkillHandlersController(SkillHandlerRepository skillHandlerRepository, IConfiguration configuration) : base(skillHandlerRepository)
        {
            this.skillHandler = skillHandlerRepository;
            this._configuration = configuration;
        }
    }
}
