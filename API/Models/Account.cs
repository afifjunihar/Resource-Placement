using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
	[Table("TB_M_Account")]
	public class Account
	{
		[Key]
		public string User_Id { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }

		[JsonIgnore]
		public virtual ICollection<AccountRole> AccountRole { get; set; }
		[JsonIgnore]
		public virtual User User { get; set; }
	}
}
