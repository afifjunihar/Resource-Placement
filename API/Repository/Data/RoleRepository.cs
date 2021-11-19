using API.Context;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class RoleRepository : GeneralRepository<ResourceContext, Role, int>
    {
        private readonly ResourceContext rContext;
        public RoleRepository(ResourceContext rContext) : base(rContext)
        {
            this.rContext = rContext;
        }

    }
}
