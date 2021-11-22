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
	}
}
