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
    public class RolesController : BaseController<Role, RoleRepository, int>
    {
        public readonly RoleRepository role;
        public IConfiguration _configuration;
        public RolesController(RoleRepository roleRepository, IConfiguration configuration) : base(roleRepository)
        {
            this.role = roleRepository;
            this._configuration = configuration;
        }
    }
}
