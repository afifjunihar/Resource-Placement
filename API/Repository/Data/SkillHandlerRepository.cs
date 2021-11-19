using API.Context;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class SkillHandlerRepository : GeneralRepository<ResourceContext, SkillHandler, int>
    {
       private readonly ResourceContext shContext;
        public SkillHandlerRepository(ResourceContext shContext) : base(shContext)
        {
            this.shContext = shContext;
        }
    }
}
