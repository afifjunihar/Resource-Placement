using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Models.ViewModels
{
    public class EmailVM
    {
		public string Sender_Alias { get; set; }
		public string Interview_Action { get; set; }
		public DateTime? Tanggal { get; set; }
		public string Nama { get; set; }
		public string Project_Name { get; set; }
		public string Jobs { get; set; }
		public string Note { get; set; }
	}
}
