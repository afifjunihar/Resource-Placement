using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.ViewModels
{
    public class ProfileVM
    {
        public string User_Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Gender Gender { get; set; }
        public CandidateStatus User_Status { get; set; }
        public string Role_Name { get; set; }
   
    }
}
