using API.Base;
using API.Models;
using API.Models.ViewModels;
using API.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, int>
    {
        public readonly AccountRepository account;
        public IConfiguration _configuration;
        public AccountsController(AccountRepository accountRepository, IConfiguration configuration) : base(accountRepository)
        {
            this.account = accountRepository;
            this._configuration = configuration;
        }
		[Route("Login")]
		[HttpPost]
		public ActionResult Login(LoginVM loginVM)
		{
			var result = account.Login(loginVM);
			if (result == 1)
			{
				return NotFound("Email atau Username belum terdaftar");
			}
			else if (result == 2)
			{
				return BadRequest(new JWTokenVM { Message = "Password salah", Token = null });
			}
			// Implement JWT
			var data = new LoginDataVM
			{
				UserId = account.GetUserID(loginVM),
				Fullname = account.GetFullName(loginVM),
				Roles = account.GetRole(loginVM)
			};

			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, data.Fullname),
				new Claim(ClaimTypes.Sid, data.UserId)
			};

			foreach (var item in data.Roles)
			{
				claims.Add(new Claim("roles", item.ToString()));
			}

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
			var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var token = new JwtSecurityToken(
					_configuration["Jwt:Issuer"],
					_configuration["Jwt:Audience"],
					claims,
					expires: DateTime.UtcNow.AddMinutes(60),
					signingCredentials: signIn
				);
			var idToken = new JwtSecurityTokenHandler().WriteToken(token);
			claims.Add(new Claim("TokenSecurity", idToken.ToString()));
			return Ok(new JWTokenVM { Message = "Login Sukses", Token = idToken });
		}
	}
}
