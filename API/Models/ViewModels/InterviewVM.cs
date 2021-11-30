using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.ViewModels
{
	public class InterviewVM
	{
		public DateTime Interview_Date { get; set; }
		public InterviewResult Result { get; set; }
		public string Description { get; set; } = "No Description";
		public string ReadBy { get; set; }
		public string User_Id { get; set; }
		public int Project_Id { get; set; }


	}
}
