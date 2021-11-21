using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
	[Table("TB_T_AccountRole")]
	public class AccountRole
	{
		[Key]
		public int Account_Roles_Id { get; set; }

		public int Account_Id { get; set; }
		public int Role_Id { get; set; }


		[JsonIgnore]
		[ForeignKey("Role_Id")]
		public virtual Role Role { get; set; }

		[JsonIgnore]
		[ForeignKey("Account_Id")]
		public virtual Account Account { get; set; }
	}
}
