using API.Context;
using API.Models;
using API.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class UserRepository : GeneralRepository<ResourceContext, User, string>
    {
        private readonly ResourceContext uContext;
        public UserRepository(ResourceContext uContext) : base(uContext)
        {
            this.uContext = uContext;
        }

      public int Register(RegisterVM registerVM)
      {
         var user = new User
         {
            User_Id = registerVM.User_Id,
				FirstName = registerVM.FirstName,
            LastName = registerVM.LastName,
            Email = registerVM.Email,
            Phone = registerVM.Phone,
            Gender = registerVM.Gender,
            User_Status = registerVM.User_Status,
            Manager_Id = registerVM.Manager_Id,
            Account = new Account
            {
               Username = registerVM.Username,
               Password = registerVM.Password
            }
         };
         var accountRoles = new AccountRole
         {
            Role_Id = 1
         };

         uContext.Users.Add(user);
         uContext.AccountRoles.Add(accountRoles);
         uContext.SaveChanges();
         return 0;
      }

   }
}
