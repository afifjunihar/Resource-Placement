using API.Context;
using API.Hashing_Password;
using API.Library;
using API.Library.Email;
using API.Models;
using API.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace API.Repository.Data
{
    public class UserRepository : GeneralRepository<ResourceContext, User, string>
    {
        private readonly ResourceContext uContext;
        private readonly IEmailSender _emailSender;
        public UserRepository(ResourceContext uContext, IEmailSender emailSender) : base(uContext)
        {
            this.uContext = uContext;
            this._emailSender = emailSender;
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
            //else if (registerVM.Manager_Id == null || registerVM.Manager_Id == string.Empty)
            //{
            //    return 5;
            //}
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
                    Manager_Id = "M0001",
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
                              User_Status = "Manager"
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
                              User_Status ="Trainer"
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
                              User_Status ="Client"
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
        public dynamic CandidateSkill(KeyVM keyVM) 
        {

            var listUser = uContext.Users.ToList();
            var listSkillHandlers = uContext.SkillHandlers.ToList();
            var listSKill = uContext.Skills.ToList();
            var getData = from a in listUser
                          join b in listSkillHandlers on a.User_Id equals b.User_Id
                          join c in listSKill on b.Skill_Id equals c.Skill_Id
                          where a.User_Id == keyVM.KeyStr && a.User_Status == CandidateStatus.Free
                          orderby b.Score descending
                          select new
                          {
                             c.Skill_Name,
                             b.Score
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
        public dynamic CandidateSkill() 
        {
            var listUser = uContext.Users.ToList();
            var listSkillHandlers = uContext.SkillHandlers.ToList();
            var listSKill = uContext.Skills.ToList();
            var ScoreFE = from a in listUser
                          join b in listSkillHandlers on a.User_Id equals b.User_Id
                          join c in listSKill on b.Skill_Id equals c.Skill_Id
                          where c.Skill_Name == "Front-End"
                          select new
                          {
                              Fullname = a.FirstName + " " + a.LastName,
                              FrontEnd = b.Score
                          };

            var ScoreBE = from a in listUser
                          join b in listSkillHandlers on a.User_Id equals b.User_Id
                          join c in listSKill on b.Skill_Id equals c.Skill_Id
                          where c.Skill_Name == "Back-End"
                          select new
                          {
                              Fullname = a.FirstName + " " + a.LastName,
                              BackEnd = b.Score
                          };

            var ScoreFS = from a in listUser
                          join b in listSkillHandlers on a.User_Id equals b.User_Id
                          join c in listSKill on b.Skill_Id equals c.Skill_Id
                          where c.Skill_Name == "Full-Stack"
                          select new
                          {
                              Fullname = a.FirstName + " " + a.LastName,
                              FullStack = b.Score
                          };
            var ScoreFC = from a in listUser
                          join b in listSkillHandlers on a.User_Id equals b.User_Id
                          join c in listSKill on b.Skill_Id equals c.Skill_Id
                          where c.Skill_Name == "Fundamental C#"
                          select new
                          {
                              Fullname = a.FirstName + " " + a.LastName,
                              Fundamental = b.Score
                          };


            var getData = from a in ScoreBE
                          join b in ScoreFC on a.Fullname equals b.Fullname 
                          join c in ScoreFE on a.Fullname equals c.Fullname
                          join d in ScoreFS on a.Fullname equals d.Fullname 
                          select new
                          {
                              Fullname = a.Fullname,
                              FundamentalC = b.Fundamental,
                              BackEnd = a.BackEnd,
                              FrontEnd = c.FrontEnd,
                              FullStack = d.FullStack,
                              score = (b.Fundamental + c.FrontEnd + d.FullStack + a.BackEnd)/4 
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

        public int lupaPassword(KeyVM key) 
        {
            var entitybyEmail  = uContext.Users.Where(p => p.Email == key.KeyStr).FirstOrDefault();          
            var entitybyUname = uContext.Accounts.Where(p => p.Username == key.KeyStr).FirstOrDefault();

            if (entitybyEmail != null)
            {
                string password = Password.Generate(15, 3);
                var User = uContext.Users.Where(p => p.Account_Id == entitybyEmail.Account_Id).FirstOrDefault();

                // Send Email
                var Payload = new Message
                (
                   //updateUser.Email,
                   "afifjunihar@gmail.com",
                    "Lupa Password",
                    new EmailVM
                    {
                        Sender_Alias = "Admin Metrodata",
                        Action = "Reset Password",
                        Nama = entitybyEmail.FirstName + " " + entitybyEmail.LastName,     
                        Refresh_Password = password
                    }
                );
                _emailSender.SendEmailAsync(Payload);
                // End of Send Email  

                Account Account = uContext.Accounts.Find(entitybyEmail.Account_Id);
                Account.Password = Hashing.HashPassword(password);
                uContext.Entry(Account).State = EntityState.Modified;
                uContext.SaveChanges();              
                return 0;
            }

            else if (entitybyUname != null)
            {
                string password = Password.Generate(15, 3);
                var User = uContext.Users.Where(p => p.Account_Id == entitybyUname.Account_Id).FirstOrDefault();

                // Send Email
                var Payload = new Message
                (
                    //updateUser.Email,
                    "afifjunihar@gmail.com",
                    "Lupa Password",
                    new EmailVM
                    {
                        Sender_Alias = "Admin Metrodata",
                        Nama = User.FirstName + " " + User.LastName,                
                        Action = "Reset Password",
                        Refresh_Password = password
                    }
                );
                _emailSender.SendEmailAsync(Payload);
                // End of Send Email


                Account Account = uContext.Accounts.Find(User.Account_Id);
                Account.Password = Hashing.HashPassword(password);
                uContext.Entry(Account).State = EntityState.Modified;
                uContext.SaveChanges();
                return 0;
            } 
            
            else
            {
                return 1;
            }
        }

    }
}
