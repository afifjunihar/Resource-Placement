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
            var result1 = user.Register(registerVM);
            var result2 = user.AssignCandidate(registerVM);
            if (result1 == 0 && result2 == 0)
            {
                return Ok();
            }
            else 
            {
                return BadRequest();
            }            
        }

        [HttpPost]
        [Route("Registration/Client")]
        public ActionResult RegisterClient(RegisterVM registerVM)
        {
            var result1 = user.Register(registerVM);
            var result2 = user.AssignClient(registerVM);
            if (result1 == 0 && result2 == 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [Route("Registration/Trainer")]
        public ActionResult RegisterTrainer(RegisterVM registerVM)
        {
            var result1 = user.Register(registerVM);
            var result2 = user.AssignTrainer(registerVM);
            if (result1 == 0 && result2 == 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [Route("Registration/Manager")]
        public ActionResult RegisterManager(RegisterVM registerVM)
        {
            var result1 = user.Register(registerVM);
            var result2 = user.AssignManager(registerVM);
            if (result1 == 0 && result2 == 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }


        [HttpGet]
        [Route("CandidateProfile")]
        public ActionResult CandidateProfile() 
        {
            try
            {
                var result = user.CandidateProfile();
                return Ok(result);

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("ManagerProfile")]
        public ActionResult ManagerProfile()
        {
            try
            {
                var result = user.ManagerProfile();
                return Ok(result);

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("ClientProfile")]
        public ActionResult ClientProfile()
        {
            try
            {
                var result = user.ClientProfile();
                return Ok(result);

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("TrainerProfile")]
        public ActionResult TrainerProfile()
        {
            try
            {
                var result = user.TrainerProfile();
                return Ok(result);

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("Profile/{UserId}")]
        public ActionResult GetProfileUser(string UserId)
        {
            try
            {
                var result = user.Profile(UserId);
                return Ok(result);

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("Profile/Skill/{Id}")]
        public ActionResult GetProfile(string Id)
        {
            var result = user.CandidateSkill(Id);
            return Ok(result);
        }

        [HttpGet]
        [Route("Profile/ListSkill")]
        public ActionResult ListCandidate()
        {
            var result = user.CandidateSkill();
            return Ok(result);
        }

        [HttpPost]
        [Route("lupapassword")]
        public ActionResult lupapassword(KeyVM key)
        {
            var result = user.lupaPassword(key);
            return Ok(result);
        }
    }
}
