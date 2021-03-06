using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
	[Table("TB_M_User")]
	public class User
	{
		[Key]
		public string User_Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public Gender Gender { get; set; }
		public CandidateStatus User_Status { get; set; }
		public int Account_Id { get; set; }


		[ForeignKey("Manager_Id")]
		public string Manager_Id { get; set; }


		[JsonIgnore]
		[ForeignKey("Account_Id")]
		public virtual Account Account { get; set; }

		[JsonIgnore]
		public virtual ICollection<SkillHandler> SkillHandler { get; set; }

		[JsonIgnore]
		public virtual ICollection<Interview> Interview { get; set; }

	}

	public enum Gender
	{
		Male,
		Female
	}

	public enum CandidateStatus
	{
		Free,
		Hired,
		OnProcess
	}

}
