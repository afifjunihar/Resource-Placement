using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
	[Table("TB_M_Skill")]
	public class Skill
	{
		[Key]
		public int Skill_Id { get; set; }
		public string Skill_Name { get; set; }

		[JsonIgnore]
		public virtual ICollection<SkillHandler> SkillHandler { get; set; }
	}
}
