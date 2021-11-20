using API.Context;
using API.Models;
using API.Models.ViewModels;
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
            var checkSkill = sContext.SkillHandlers.Where(p => p.Skill_Id == addskill.Skill_Id).FirstOrDefault();
            if (addskill.User_Id == string.Empty || addskill.Skill_Id == 0)
            {
                return 1;
            }
            else if (checkSkill != null)
            {
                return 2;
            }
            else 
            {
            
                var skill = new SkillHandler
                {
                    Score = addskill.Score_skill,
                    User_Id = addskill.User_Id,
                    Skill_Id = addskill.Skill_Id
                };

                sContext.SkillHandlers.Add(skill);
                sContext.SaveChanges();
                return 0;
            }
        }

    }

}
