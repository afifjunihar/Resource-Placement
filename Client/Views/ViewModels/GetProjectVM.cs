using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Views.ViewModels
{
    public class GetProjectVM
    {
		public int Project_Id { get; set; }
		public string Project_Name { get; set; }
		public int Capacity { get; set; }
		public int Current_Capacity { get; set; }
		public string Required_Skill { get; set; }
		public Status Status { get; set; }
		public string Creator_Id { get; set; }
	}
}
