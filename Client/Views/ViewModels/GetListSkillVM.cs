using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Views.ViewModels
{
    public class GetListSkillVM
    {
        public string user_Id { get; set; }
        public string FullName { get; set; }
        public int FundamentalC { get; set; }
        public int BackEnd { get; set; }
        public int FrontEnd { get; set; }
        public int fullStack { get; set; }
        public string Score { get; set; }
    }
}
