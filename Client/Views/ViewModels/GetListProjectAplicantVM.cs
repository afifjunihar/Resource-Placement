using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Views.ViewModels
{
    public class GetListProjectAplicantVM
    {
        public string User_Id { get; set; }
        public string FullName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public int score { get; set; }
    }
}
