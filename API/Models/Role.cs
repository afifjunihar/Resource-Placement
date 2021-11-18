using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
	[Table("TB_M_Role")]
	public class Role
	{
		[Key]
		public int Role_Id { get; set; }
		public string Role_Name { get; set; }

		/* --------- Relation ---------- */
		[JsonIgnore]
		public virtual ICollection<AccountRole> AccountRole { get; set; }
	}
}
