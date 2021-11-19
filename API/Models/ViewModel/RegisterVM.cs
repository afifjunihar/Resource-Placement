using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.ViewModel
{
	public class RegisterVM
	{
      public string User_Id { get; set; }
		public string FirstName { get; set; }
      public string LastName { get; set; }
      public string Email { get; set; }
      public string Phone { get; set; }
      public Gender Gender { get; set; }
      public CandidateStatus User_Status { get; set; }
      public int Manager_Id { get; set; }
      //public int Account_Id { get; set; }
      public string Username { get; set; }
      public string Password { get; set; }
   }
}
