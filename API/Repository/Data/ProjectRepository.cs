using API.Context;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class ProjectRepository : GeneralRepository<ResourceContext, Project, int>
    {
		public ProjectRepository(ResourceContext resourceContext) : base (resourceContext)
		{

		}
    }
}
