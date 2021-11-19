using API.Context;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class AccountRoleRepository : GeneralRepository<ResourceContext, AccountRole, int>
    {
        private readonly ResourceContext arContext;
        public AccountRoleRepository(ResourceContext arContext) : base(arContext) 
        {
            this.arContext = arContext;
        }

        
    }
}
