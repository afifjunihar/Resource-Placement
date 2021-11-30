using API.Context;
using API.Hashing_Password;
using API.Models;
using API.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class AccountRepository : GeneralRepository<ResourceContext, Account, int>
    {
        private readonly ResourceContext aContext;
        public AccountRepository(ResourceContext aContext) : base(aContext) 
        {
            this.aContext = aContext;
        }

		public int Login(LoginVM loginVM)
		{
			var emailExist = aContext.Users.Where(fn => fn.Email == loginVM.EmailOrUsername).FirstOrDefault();
			var usernameExist = aContext.Accounts.Where(fn => fn.Username == loginVM.EmailOrUsername).FirstOrDefault();
			if (emailExist != null)
			{
				var passwordByEmail = aContext.Accounts.Find(emailExist.Account_Id);

				bool isVerify = Hashing.ValidatePassword(loginVM.Password, passwordByEmail.Password);
				if (isVerify)
				{
					return 0;
				}
				return 2;
			}
			else if (usernameExist != null)
			{
				var passwordByUsername = usernameExist.Password;
				bool isVerify = Hashing.ValidatePassword(loginVM.Password, passwordByUsername);
				if (isVerify)
				{
					return 0;
				}
				return 2;

			}
			return 1;
		}

		public string[] GetRole(LoginVM loginVM)
		{
			var emailExist = aContext.Users.Where(fn => fn.Email == loginVM.EmailOrUsername).FirstOrDefault();
			var usernameExist = aContext.Accounts.Where(fn => fn.Username == loginVM.EmailOrUsername).FirstOrDefault();

			if (emailExist != null)
			{
				var roleByEmail = aContext.AccountRoles.Where(fn => fn.Account_Id == emailExist.Account_Id).ToList();
				List<string> result = new List<string>();
				foreach (var item in roleByEmail)
				{
					result.Add(aContext.Roles.Where(fn => fn.Role_Id == item.Role_Id).FirstOrDefault().Role_Name);
				}
				return result.ToArray();
			}
			else
			{
				var roleByUsername = aContext.AccountRoles.Where(fn => fn.Account_Id == usernameExist.Account_Id).ToList();
				List<string> result = new List<string>();
				foreach (var item in roleByUsername)
				{
					result.Add(aContext.Roles.Where(fn => fn.Role_Id == item.Role_Id).FirstOrDefault().Role_Name);
				}
				return result.ToArray();
			}
		}

		public string GetFullName(LoginVM loginVM)
		{
			User emailExist = aContext.Users.Where(fn => fn.Email == loginVM.EmailOrUsername).FirstOrDefault();
			Account usernameExist = aContext.Accounts.Where(fn => fn.Username == loginVM.EmailOrUsername).FirstOrDefault();

			if (emailExist != null)
			{
				return emailExist.FirstName + " " + emailExist.LastName;
			}
			else
			{
				User dataUser = aContext.Users.Where(fn => fn.Account_Id == usernameExist.Account_Id).FirstOrDefault();
				return dataUser.FirstName + " " + dataUser.LastName;
			}
		}

		public string GetUserID(LoginVM loginVM)
		{
			User emailExist = aContext.Users.Where(fn => fn.Email == loginVM.EmailOrUsername).FirstOrDefault();
			Account usernameExist = aContext.Accounts.Where(fn => fn.Username == loginVM.EmailOrUsername).FirstOrDefault();

			if (emailExist != null)
			{
				return emailExist.User_Id;
			}
			else
			{
				User dataUser = aContext.Users.Where(fn => fn.Account_Id == usernameExist.Account_Id).FirstOrDefault();
				return dataUser.User_Id;
			}
		}

	}
}
