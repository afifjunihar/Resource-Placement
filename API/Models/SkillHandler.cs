using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
	[Table("TB_T_Skill_Handler")]
	public class SkillHandler
	{
		[Key]
		public int Skill_Handler_Id { get; set; }
		public int Score { get; set; }
		public int User_Id { get; set; }
		public int Skill_Id { get; set; }

		// Relation Above
	}
}
