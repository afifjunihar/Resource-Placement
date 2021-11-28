using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
	[Table("TB_M_Project")]
	public class Project
	{
		[Key]
		public int Project_Id { get; set; }
		public string Project_Name { get; set; }
		public int Capacity { get; set; }
		public int Current_Capacity { get; set; } = 0;
		public string Required_Skill { get; set; }
		public Status Status { get; set; } = Status.Open;
		public string Creator_Id { get; set; }

		[JsonIgnore]
		public virtual ICollection<Interview> Interview { get; set; }

		[JsonIgnore]
		[ForeignKey("Creator_Id")]
		public virtual User User { get; set; }
	}

	public enum Status
	{
		Open,
		Closed
	}
}
