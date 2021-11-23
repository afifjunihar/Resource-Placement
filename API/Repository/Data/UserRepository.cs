using API.Context;
using API.Library;
using API.Models;
using API.Models.ViewModels;
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
            var checkEmail = uContext.Users.Where(p => p.Email == registerVM.Email).FirstOrDefault();
            var checkPhone = uContext.Users.Where(p => p.Phone == registerVM.Phone).FirstOrDefault();
            var checkNik = uContext.Users.Find(registerVM.User_Id);
            if (registerVM.User_Id == string.Empty)
            {
                return 1;
            }
            else if (checkNik != null)
            {
                return 2;
            }
            else if (checkEmail != null)
            {
                return 3;
            }
            else if (checkPhone != null)
            {
                return 4;
            }
            else if (registerVM.Manager_Id == string.Empty)
            {
                return 4;
            }
            else
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
                            Password = Hashing.HashPassword(registerVM.Password)
                    }
              };
        
              uContext.Users.Add(user);
              uContext.SaveChanges();    
              return 0;
            }
        }
        public int AssignCandidate(RegisterVM registerVM) 
        {
            var AccountId = uContext.Users.Find(registerVM.User_Id).Account_Id;
            var candidate = new AccountRole
            {
                Account_Id = AccountId,
                Role_Id = 1
            };
            uContext.AccountRoles.Add(candidate);
            uContext.SaveChanges();
            return 0;
        }
        public int AssignClient(RegisterVM registerVM)
        {
            var AccountId = uContext.Users.Find(registerVM.User_Id).Account_Id;
            var candidate = new AccountRole
            {
                Account_Id = AccountId,
                Role_Id = 3
            };
            uContext.AccountRoles.Add(candidate);
            uContext.SaveChanges();
            return 0;
        }
        public int AssignTrainer(RegisterVM registerVM)
        {
            var AccountId = uContext.Users.Find(registerVM.User_Id).Account_Id;
            var candidate = new AccountRole
            {
                Account_Id = AccountId,
                Role_Id = 2
            };
            uContext.AccountRoles.Add(candidate);
            uContext.SaveChanges();
            return 0;
        }
        public int AssignManager(RegisterVM registerVM)
        {
            var AccountId = uContext.Users.Find(registerVM.User_Id).Account_Id;
            var candidate = new AccountRole
            {
                Account_Id = AccountId,
                Role_Id = 4
            };
            uContext.AccountRoles.Add(candidate);
            uContext.SaveChanges();
            return 0;
        }


        public object Profile(string UserId) 
        {
            var listUser = uContext.Users.ToList();
            var getData = from a in listUser
                          where a.User_Id == UserId
                          select new
                          {
                              a.User_Id,
                              Fullname = a.FirstName + " " + a.LastName,
                              a.Email,
                              a.Gender,
                              a.Phone,
                              a.User_Status
                          };

            int hitungData = getData.Count();
            if (hitungData == 0)
            {
                string checkData = "Tidak ditemukan Data pada Database";
                return checkData;
            }
            else
            {
                return getData;
            }
        }
        public dynamic CandidateProfile()
        {
            var listUser = uContext.Users.ToList();
            var listAccountRoles = uContext.AccountRoles.ToList();
            var listRoles = uContext.Roles.ToList();
            var getData = from a in listUser
                          join b in listAccountRoles on a.Account_Id equals b.Account_Id
                          join c in listRoles on b.Role_Id equals c.Role_Id 
                          where c.Role_Id == 1                        
                          select new
                          {
                              a.User_Id,
                              Fullname = a.FirstName + " " + a.LastName,
                              a.Email,
                              a.Gender,
                              a.Phone,
                              a.User_Status        
                          };

            int hitungData = getData.Count();
            if (hitungData == 0)
            {
                string checkData = "Tidak ditemukan Data pada Database";
                return checkData;
            }
            else
            {
                return getData;
            }
        }
        public object ManagerProfile()
        {
            var listUser = uContext.Users.ToList();
            var listAccountRoles = uContext.AccountRoles.ToList();
            var listRoles = uContext.Roles.ToList();
            var getData = from a in listUser
                          join b in listAccountRoles on a.Account_Id equals b.Account_Id
                          join c in listRoles on b.Role_Id equals c.Role_Id
                          where c.Role_Id == 4
                          select new
                          {
                              a.User_Id,
                              Fullname = a.FirstName + " " + a.LastName,
                              a.Email,
                              a.Gender,
                              a.Phone,
                              a.User_Status
                          };

            int hitungData = getData.Count();
            if (hitungData == 0)
            {
                string checkData = "Tidak ditemukan Data pada Database";
                return checkData;
            }
            else
            {
                return getData;
            }
        }
        public object TrainerProfile()
        {
            var listUser = uContext.Users.ToList();
            var listAccountRoles = uContext.AccountRoles.ToList();
            var listRoles = uContext.Roles.ToList();
            var getData = from a in listUser
                          join b in listAccountRoles on a.Account_Id equals b.Account_Id
                          join c in listRoles on b.Role_Id equals c.Role_Id
                          where c.Role_Id == 2
                          select new
                          {
                              a.User_Id,
                              Fullname = a.FirstName + " " + a.LastName,
                              a.Email,
                              a.Gender,
                              a.Phone,
                              a.User_Status
                          };

            int hitungData = getData.Count();
            if (hitungData == 0)
            {
                string checkData = "Tidak ditemukan Data pada Database";
                return checkData;
            }
            else
            {
                return getData;
            }
        }
        public object ClientProfile()
        {
            var listUser = uContext.Users.ToList();
            var listAccountRoles = uContext.AccountRoles.ToList();
            var listRoles = uContext.Roles.ToList();
            var getData = from a in listUser
                          join b in listAccountRoles on a.Account_Id equals b.Account_Id
                          join c in listRoles on b.Role_Id equals c.Role_Id
                          where c.Role_Id == 3
                          select new
                          {
                              a.User_Id,
                              Fullname = a.FirstName + " " + a.LastName,
                              a.Email,
                              a.Gender,
                              a.Phone,
                              a.User_Status
                          };

            int hitungData = getData.Count();
            if (hitungData == 0)
            {
                string checkData = "Tidak ditemukan Data pada Database";
                return checkData;
            }
            else
            {
                return getData;
            }
        }


    }
}
