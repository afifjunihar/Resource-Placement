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
		public int User_Id { get; set; }
		public int Project_Id { get; set; }

	}

	public enum InterviewResult
	{
		Accepted,
		Rejected,
		Waiting
	}
}
