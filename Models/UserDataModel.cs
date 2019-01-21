using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecpMgmtWebApi.Models
{
	public class UserDataModel
	{
		public int UserId { get; set; }
		public string UserName { get; set; }
		public string UserPwd { get; set; }
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public string UserEmail { get; set; }
		public string UserPhone { get; set; }
		public Nullable<System.DateTime> DeletedDate { get; set; }

		public int[]  RoleId { get; set; }
		//public string RoleName { get; set; }
	}
}