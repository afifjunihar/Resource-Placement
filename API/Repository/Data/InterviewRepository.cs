using API.Context;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class InterviewRepository : GeneralRepository<ResourceContext, Interview, int>
    {
        private readonly ResourceContext iContext;
        public InterviewRepository(ResourceContext iContext) : base(iContext) 
        {
            this.iContext = iContext;        
        }
    }
}
