using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.ViewModels
{
    public class AddSkillVM
    {
        public string User_Id { get; set; }
        public int Skill_Id { get; set; }
        public int Score_skill { get; set; }
    }
}
