using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.ViewModels
{
    public class AddSkillVM
    {
        public string User_Id { get; set; }
        public int fundamentalCScore { get; set; }
        public int BackEndScore { get; set; }
        public int FrontEndScore { get; set; }
        public int FullstackScore { get; set; }
    }
}
