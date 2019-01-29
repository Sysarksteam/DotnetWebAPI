using System;
using System.Collections;
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
						  select new { a.RoleId, a.RoleName }).ToList();
			return Ok(result);
		}

		// GET: api/User/GetUserTbls
		[HttpGet]
		[ActionName("GetUserTbls")]
		public IHttpActionResult GetUserTbls()
		{
			List<UserTbl> userTbls = new List<UserTbl>();
			var result = (from a in db.UserTbls
						  where a.DeletedDate.Equals(null)
						  select new
						  {
							  a.UserId,
							  a.UserName,
							  a.UserPwd,
							  a.FirstName,
							  a.LastName,
							  a.UserEmail,
							  a.UserPhone
						  }).ToList();
			return Ok(result);
		}

//==========================================================================

		// POST: api/User/AddUserRoleTbl
		[HttpPost]
		[ActionName("AddUserRoleTbl")]
		public HttpResponseMessage AddUserRoleTbl(UserDataModel userDataModal)
		{
			try
			{
				UserTbl userTbl = new UserTbl();
				userTbl.UserName = userDataModal.UserName;
				userTbl.UserPwd = userDataModal.UserPwd;
				userTbl.LastName = userDataModal.LastName;
				userTbl.FirstName = userDataModal.FirstName;
				userTbl.UserEmail = userDataModal.UserEmail;
				userTbl.UserPhone = userDataModal.UserPhone;

				int[] roleId = userDataModal.RoleId;
				db.UserTbls.Add(userTbl);
				db.SaveChanges();

				int latestUserId = userTbl.UserId;

				foreach (int items in roleId)
				{
					UserRoleTbl userRoleTbl = new UserRoleTbl();
					userRoleTbl.UserId = latestUserId;
					userRoleTbl.RoleId = items;
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
		public HttpResponseMessage DeleteUser(UserDataModel userDataModel)
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
					return Request.CreateResponse(HttpStatusCode.OK,
						"userid = " + id.ToString() + " is deleted successfully...!!");
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
			//int val = 0;
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

					int[] roleId = userDataModel.RoleId;
					int id1 = id;

					foreach (int items in roleId)
					{
						UserRoleTbl userRoleTbl = new UserRoleTbl();
						userRoleTbl.UserId = id1;
						userRoleTbl.RoleId = items;
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

//========================================================================================
		//POST: api/User/AddTempAccessPermissionTbl
		[HttpPost]
		[ActionName("AddTempAccessPermissionTbl")]
		public IHttpActionResult AddTempAccessPermissionTbl(AccessPermissionTbl accessPermission)
		{
			db.AccessPermissionTbls.Add(accessPermission);
			db.SaveChanges();

			return Ok(accessPermission);
		}
//=============================================================================================
		// POST: api/User/DelTempAccessPermissionTbl
		[HttpPost]
		[ActionName("DelTempAccessPermissionTbl")]
		public IHttpActionResult DelTempAccessPermissionTbl(AccessPermissionTbl accessPermission)
		{
			AccessPermissionTbl accessPermissiontbl = new AccessPermissionTbl();

			int userId = accessPermission.UserId;
			int accessId = accessPermission.AccessId;
			int permissionId = accessPermission.PermissionId;
			AccessPermissionTbl accessPermissionTbl1 = db.AccessPermissionTbls.Find(userId, accessId, permissionId);
			db.AccessPermissionTbls.Remove(accessPermissionTbl1);
			db.SaveChanges();

			return Ok(accessPermissionTbl1);
		}

//===========================Not Complete========================================
		[HttpPost]
		[ActionName("AddAccessPermissionTbl")]
		public IHttpActionResult AddAccessPermissionTbl(UserDataModel userDataModal)
		{
			try
			{
				int role = userDataModal.role;
				var result = (from a in db.RoleAccessPermissionTbls
							  where a.RoleId.Equals(role)
							  select new
							  {
								  a.AccessId,
								  a.PermissionId
							  }).ToList();

				int accessId = 0, permissionId = 0;
		//		var result1, result2;
				if (result.Count() > 0)
				{
					for (int i = 0; i < result.Count(); i++)
					{
						AccessPermissionTbl accessPermissionTbl = new AccessPermissionTbl();
						accessPermissionTbl.AccessId = result.ToList()[i].AccessId;
						accessPermissionTbl.PermissionId = result.ToList()[i].PermissionId;
						accessPermissionTbl.UserId = userDataModal.UserId;
						accessId = result.ToList()[i].AccessId;
						permissionId = result.ToList()[i].PermissionId;
						db.AccessPermissionTbls.Add(accessPermissionTbl);

						//result1 = (from a in db.AccessTbls
						//		   where a.AccessId.Equals(accessId)
						//		   select new
						//		   {
						//			   a.AccessName
						//		   }).ToList();

						//result2 = (from b in db.PermissionTbls
						//		   where b.PermissionId.Equals(permissionId)
						//		   select new
						//		   {
						//			   b.PermissionId
						//		   }).ToList();
					}
				}
				db.SaveChanges();
				return Ok(result);
			}
			catch (Exception)
			{
				return Content(HttpStatusCode.NotFound, "RoleId & UserId not found");
			}
		}


//===========================================================================================

	
	}
}
