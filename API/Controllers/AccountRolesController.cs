
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
    public class AccountRolesController : BaseController<AccountRole, AccountRoleRepository, int>
    {
        public readonly AccountRoleRepository accountRole;
        public IConfiguration _configuration;
        public AccountRolesController(AccountRoleRepository accountRoleRepository, IConfiguration configuration) : base(accountRoleRepository)
        {
            this.accountRole = accountRoleRepository;
            this._configuration = configuration;
        }
    }
}
