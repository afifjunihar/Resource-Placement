using API.Context;
using API.Models;
using API.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
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

        public int AddSkill(AddSkillVM addskill)
        {
            var checkSkill = sContext.Users.Where(p => p.Score_Status == isScore.No && p.User_Id == addskill.User_Id).FirstOrDefault();
            if (addskill.User_Id == string.Empty )
            {
                return 1;
            }
            else if (checkSkill != null)
            {
                var fundamental = new SkillHandler
                {
                    Score = addskill.fundamentalCScore,
                    User_Id = addskill.User_Id,
                    Skill_Id = 4
                };

                sContext.SkillHandlers.Add(fundamental);
                sContext.SaveChanges();

                var backend = new SkillHandler
                {
                    Score = addskill.BackEndScore,
                    User_Id = addskill.User_Id,
                    Skill_Id = 2
                };

                sContext.SkillHandlers.Add(backend);
                sContext.SaveChanges();

                var frontend = new SkillHandler
                {
                    Score = addskill.FrontEndScore,
                    User_Id = addskill.User_Id,
                    Skill_Id = 1
                };

                sContext.SkillHandlers.Add(frontend);
                sContext.SaveChanges();

                var fullstack = new SkillHandler
                {
                    Score = addskill.FullstackScore,
                    User_Id = addskill.User_Id,
                    Skill_Id = 3
                };

                sContext.SkillHandlers.Add(fullstack);
                sContext.SaveChanges();

                User updateUser = sContext.Users.Find(addskill.User_Id);
                updateUser.Score_Status = isScore.Yes;
                sContext.Entry(updateUser).State = EntityState.Modified;
                sContext.SaveChanges();

                return 0;
            }
            else 
            {            
                return 2;
            }
        }

        public int UpdateSkill(AddSkillVM addskill)
        {
            var checkSkill = sContext.Users.Where(p => p.Score_Status == isScore.Yes && p.User_Id == addskill.User_Id).FirstOrDefault();
            if (checkSkill != null)
            {

                SkillHandler updateSkill1 = sContext.SkillHandlers.Find(1);
                updateSkill1.Score = addskill.FrontEndScore;
                sContext.Entry(updateSkill1).State = EntityState.Modified;
                sContext.SaveChanges();

                SkillHandler updateSkill2 = sContext.SkillHandlers.Find(2);
                updateSkill2.Score = addskill.BackEndScore;
                sContext.Entry(updateSkill2).State = EntityState.Modified;
                sContext.SaveChanges();

                SkillHandler updateSkill3 = sContext.SkillHandlers.Find(3);
                updateSkill3.Score = addskill.FullstackScore;
                sContext.Entry(updateSkill3).State = EntityState.Modified;
                sContext.SaveChanges();

                SkillHandler updateSkill4 = sContext.SkillHandlers.Find(4);
                updateSkill4.Score = addskill.fundamentalCScore;
                sContext.Entry(updateSkill4).State = EntityState.Modified;
                sContext.SaveChanges();

                User updateUser = sContext.Users.Find(addskill.User_Id);
                updateUser.Score_Status = isScore.Yes;
                sContext.Entry(updateUser).State = EntityState.Modified;
                sContext.SaveChanges();

                return 0;
            }
            else
            {
                return 1;
            }
        }

    }

}
