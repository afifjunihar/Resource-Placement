using API.Context;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class SkillRepository : GeneralRepository<ResourceContext, Skill, int>
    {
        private readonly ResourceContext sContext;
        public SkillRepository(ResourceContext sContext) : base(sContext)
        {
            this.sContext = sContext;
        }
    }

}
