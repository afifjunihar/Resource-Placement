using API.Context;
using API.Models;
using API.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
	public class ProjectRepository : GeneralRepository<ResourceContext, Project, int>
	{
		private readonly ResourceContext pContext;
		public ProjectRepository(ResourceContext pContext) : base(pContext)
		{
			this.pContext = pContext;
		}

		public Object Details(string Key)
		{
			int PK = Convert.ToInt32(Key);
			var detailProject = from proj in pContext.Projects
									  join usr in pContext.Users on proj.Creator_Id equals usr.User_Id
									  where proj.Project_Id == PK
									  select new { 
											proj,
											fullname = usr.FirstName + " " + usr.LastName,
									  };

			return detailProject.FirstOrDefault();
		}

		public IEnumerable<Object> Handler(string Key)
		{
			int PK = Convert.ToInt32(Key);

			string userID = (from iv in pContext.Interviews where iv.Project_Id == PK select iv.User_Id).FirstOrDefault();
			int grade = (from sk in pContext.SkillHandlers
								  where sk.User_Id == userID
								  group sk by sk.User_Id into skillGrup
								  select ((int)skillGrup.Average(o => o.Score))).FirstOrDefault();

			var projectHandler = from iv in pContext.Interviews
										join us in pContext.Users on iv.User_Id equals us.User_Id
										where iv.Project_Id == PK && iv.Interview_Result == InterviewResult.Waiting
										select new
										{
											iv.Interview_Id,
											name = us.FirstName + " " + us.LastName,
											grade = (grade > 82 ? "A" : "B" ),
											iv.Interview_Date,
											iv.Interview_Result
										};

			return projectHandler.ToList();
		}
	}
}
