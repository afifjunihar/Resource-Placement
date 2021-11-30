using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
	[Table("TB_M_Interview")]
	public class Interview
	{
		[Key]
		public int Interview_Id { get; set; }
		public DateTime Interview_Date { get; set; }
		public InterviewResult Interview_Result { get; set; }
		public string Description { get; set; }
		public string ReadBy { get; set; }
		public string User_Id { get; set; }
		public int Project_Id { get; set; }

		[JsonIgnore]
		[ForeignKey("User_Id")]
		public virtual User User { get; set; }

		[JsonIgnore]
		[ForeignKey("Project_Id")]
		public virtual Project Project { get; set; }
	}

	public enum InterviewResult
	{
		Waiting,
		Accepted,
		Rejected,
	}
}
