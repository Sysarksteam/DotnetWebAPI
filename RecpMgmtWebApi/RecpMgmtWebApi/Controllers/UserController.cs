using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RecpMgmtWebApi.Models;

namespace RecpMgmtWebApi.Controllers
{
    public class UserController : ApiController
    {
		private RcpMgmtConnString db = new RcpMgmtConnString();

		// GET: api/User/GetRoleTbls
		[ActionName("GetRoleTbls")]
		public IHttpActionResult GetRoleTbls()
		{
			var result = (from a in db.RoleTbls
						  select new { a.RoleId , a.RoleName}).ToList();
			return Ok(result);

			//int i = 0;

			//List<UserRoleTbl> myUserRoleTbl = db.UserRoleTbls.Where(x => x.UserId == i).ToList();
			
			//if (myUserRoleTbl.Count() > 0)
			//{
			//	for (int j = 0; j < myUserRoleTbl.Count(); j++)
			//	{
			//		db.UserRoleTbls.Remove(myUserRoleTbl[j]);
			//	}
			//	db.SaveChanges();
			//}
			//UserTbl myUserTbl = db.UserTbls.Where(x => x.UserId == i).FirstOrDefault();
			//myUserTbl.DeletedDate = DateTime.Now;

		}

		// GET: api/User/GetUserTbls
		[HttpGet]
		[ActionName("GetUserTbls")]
		public IEnumerable<UserTbl> GetUserTbls()
		{
			return db.UserTbls.Where(x => x.DeletedDate == null).ToList();
		}
		//==========================================================================

		// POST: api/User/AddUserRoleTbl
		[HttpPost]
		[ActionName("AddUserRoleTbl")]
		public HttpResponseMessage AddUserRoleTbl(UserDataModel userDataModal)
		{
			int val = 0;
			try
			{
				UserTbl userTbl = new UserTbl();
				userTbl.UserName = userDataModal.UserName;
				userTbl.UserPwd = userDataModal.UserPwd;
				userTbl.LastName = userDataModal.LastName;
				userTbl.FirstName = userDataModal.FirstName;
				userTbl.UserEmail = userDataModal.UserEmail;
				userTbl.UserPhone = userDataModal.UserPhone;

				string roleId = userDataModal.RoleId;
				string role = "";
				db.UserTbls.Add(userTbl);
				db.SaveChanges();

				int latestUserId = userTbl.UserId;

				while (roleId.Length > 0)
				{
					role = roleId.Contains(",") ?
						roleId.Substring(0, roleId.IndexOf(","))
						: roleId;
					val = Convert.ToInt32(role);
					if (!roleId.Contains(","))
						roleId = "";
					else
						roleId = roleId.Substring(roleId.IndexOf(",") + 1);
					UserRoleTbl userRoleTbl = new UserRoleTbl();
					userRoleTbl.UserId = latestUserId;
					userRoleTbl.RoleId = val;
					db.UserRoleTbls.Add(userRoleTbl);
				}

				db.SaveChanges();
				var message = Request.CreateResponse(HttpStatusCode.Created, userTbl);
				return message;
			}
			catch (Exception ex)
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
			}
		}
		//============================================================================
		// POST: api/User/DeleteUser
		[HttpPost]
		[ActionName("DeleteUser")]
		public HttpResponseMessage DeleteUser(UserDataModel userDataModel )
		{
			try
			{
				UserRoleTbl userRoleTbl = new UserRoleTbl();
				int id = userDataModel.UserId;
				var data = db.UserRoleTbls.FirstOrDefault(x => x.UserId == id);
				if (data == null)
				{
					return Request.CreateErrorResponse(HttpStatusCode.NotFound,
						"UserRoleTbl with userid = " + id.ToString() + " not found to delete");
				}
				else
				{
					List<UserRoleTbl> myUserRoleTbl = db.UserRoleTbls.Where(x => x.UserId == id).ToList();

					if (myUserRoleTbl.Count() > 0)
					{
						for (int j = 0; j < myUserRoleTbl.Count(); j++)
						{
							db.UserRoleTbls.Remove(myUserRoleTbl[j]);
						}
						db.SaveChanges();
					}
					UserTbl myUserTbl = db.UserTbls.Where(x => x.UserId == id).FirstOrDefault();
					myUserTbl.DeletedDate = DateTime.Now;
					db.SaveChanges();
					return Request.CreateResponse(HttpStatusCode.OK);
				}
			}
			catch (Exception ex)
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
			}
		}

		//============================================================================
		// Put: api/User/UpdateUser
		[HttpPut]
		[ActionName("UpdateUser")]
		public HttpResponseMessage UpdateUser(int id, UserDataModel userDataModel)
		{
			int val = 0;
			try
			{
				UserTbl userTbl = new UserTbl();
			//	int id = userDataModel.UserId;
				var data = db.UserTbls.FirstOrDefault(x => x.UserId == id);
				if (data == null)
				{
					return Request.CreateErrorResponse(HttpStatusCode.NotFound,
						"UserTbl with userid = " + id.ToString() + " not found to update");
				}
				else
				{
					data.LastName = userDataModel.LastName;
					data.FirstName = userDataModel.FirstName;
					data.UserEmail = userDataModel.UserEmail;
					data.UserPhone = userDataModel.UserPhone;
					db.SaveChanges();

					List<UserRoleTbl> myUserRoleTbl = db.UserRoleTbls.Where(x => x.UserId == id).ToList();

					if (myUserRoleTbl.Count() > 0)
					{
						for (int j = 0; j < myUserRoleTbl.Count(); j++)
						{
							db.UserRoleTbls.Remove(myUserRoleTbl[j]);
						}
						db.SaveChanges();
					}


					string roleId = userDataModel.RoleId;
					string role = "";
					int id1 = id;
					while (roleId.Length > 0)
					{
						role = roleId.Contains(",") ?
							roleId.Substring(0, roleId.IndexOf(","))
							: roleId;
						val = Convert.ToInt32(role);
						if (!roleId.Contains(","))
							roleId = "";
						else
							roleId = roleId.Substring(roleId.IndexOf(",") + 1);
						UserRoleTbl userRoleTbl = new UserRoleTbl();
						userRoleTbl.UserId = id1;
						userRoleTbl.RoleId = val;
						db.UserRoleTbls.Add(userRoleTbl);
					}
					db.SaveChanges();
					return Request.CreateResponse(HttpStatusCode.Created, data);
				}
			}
			catch (Exception ex)
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
			}
		}
		//============================================================================
		//GET: api/User/GetRoleIdName
		[HttpGet]
		[ActionName("GetRoleIdName")]
		public IHttpActionResult GetRoleIdName()
		{
			var result = (from a in db.RoleTbls
						  select new { a.RoleId, a.RoleName }).ToList();
			
			return Ok(result);
		}
	}
}
