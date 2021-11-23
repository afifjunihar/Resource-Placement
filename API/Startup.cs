using API.Context;
using API.Library;
using API.Library.Email;
using API.Repository.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{


			services
				.AddControllers()
				.AddNewtonsoftJson(option => {
					option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
					option.SerializerSettings.Converters.Add(new StringEnumConverter()); // Make Enum To String
					});		

			services.AddDbContext<ResourceContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("APIContext"))
			);

			services.AddScoped<AccountRepository>();
			services.AddScoped<AccountRoleRepository>();
			services.AddScoped<InterviewRepository>();
			services.AddScoped<ProjectRepository>();
			services.AddScoped<RoleRepository>();
			services.AddScoped<SkillHandlerRepository>();
			services.AddScoped<SkillRepository>();
			services.AddScoped<UserRepository>();
			services.AddScoped<Hashing>();

			// JWT
			services.AddAuthentication(auth =>
			{
				auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(option => {
				option.RequireHttpsMetadata = false;
				option.SaveToken = true;
				option.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = true,
					ValidateAudience = false,
					ValidAudience = Configuration["Jwt:Audience"],
					ValidIssuer = Configuration["Jwt:Issuer"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
					ValidateLifetime = true,
					ClockSkew = TimeSpan.Zero
				};
				});

			// Email 
			services.AddSingleton(Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
			services.AddScoped<IEmailSender, EmailSender>();

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			// JWT
			app.UseAuthentication();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
