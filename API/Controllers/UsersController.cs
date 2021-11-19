using API.Base;
using API.Models;
using API.Models.ViewModels;
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
    public class UsersController : BaseController<User, UserRepository, string>
    {

        public readonly UserRepository user;
        public IConfiguration _configuration;
        public UsersController(UserRepository userRepository, IConfiguration configuration) : base(userRepository)
        {
            this.user = userRepository;
            this._configuration = configuration;
        }

        [HttpPost]
        [Route("Registration/Candidate")]
        public ActionResult RegisterCandidate(RegisterVM registerVM) 
        {
            var result = user.RegisterCandidate(registerVM);
            if (result == 0)
            {
                return Ok();
            }
            else 
            {
                return BadRequest();
            }
            
        }

    }
}
