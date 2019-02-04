using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using RecpMgmtWebApi.Models;

namespace RecpMgmtWebApi.Controllers
{
	public class UserRoleController : ApiController
	{
		private RcpMgmtConnString db = new RcpMgmtConnString();

		//GET: api/UserRole/Test
		[HttpPost]
		[ActionName("Test")]
		public IHttpActionResult Test(UserDataModel userDataModel)
		{
			int id = userDataModel.AccessId;

			var result1 = (from a in db.AccessTbls
						   where a.AccessId.Equals(id)
						  select new {  a.AccessName }).ToList();

			var result2 = (from b in db.AccessTbls
						   select new { b.AccessId, b.AccessName }).ToList();

			//var result3 = (from c in db.PermissionTbls
			//			   select new { c.PermissionId, c.PermissionName }).ToList();

			return Ok(result1);
		}

		public class DisplayData
		{

		}



	}
}

//	//[EnableCors(origins: "http://mywebclient.azurewebsites.net", headers: "*", methods: "*")]
//	public class UserRoleController : ApiController
//	{
//		private RcpMgmtConnString db = new RcpMgmtConnString();

//		// GET: api/UserRole/GetUsers
//		[ActionName("GetUsers")]
//		public IHttpActionResult GetUserTbls()
//		{
//			//return Ok(db.UserTbls.Select(x => new UserTbl { UserId = x.UserId, FirstName = x.FirstName }).ToList());
//			var result = (from a in db.UserTbls
//						  select new { a.FirstName, a.LastName, a.UserName, a.UserId }).ToList();

//			return Ok(result);
//		}

//		//// GET: api/UserRole/ValidateUser?userName="username"
//		//[HttpGet]
//		//[ActionName("ValidateUser")]
//		//public IHttpActionResult ValidateUser(string userName)
//		//{
//		//	//return Ok(db.UserTbls.Select(x => new UserTbl { UserId = x.UserId, FirstName = x.FirstName }).ToList());
//		//	var result = (from a in db.UserTbls
//		//				  where a.UserName == userName
//		//				  select new { a.UserName, a.UserId }).ToList();
//		//	if (result.Count > 0)
//		//		return Ok(result.FirstOrDefault().UserId);
//		//	else
//		//		return Ok(-1);
//		//}

//		//[HttpGet]
//		//// GET: api/UserRole/ValidatePassword?UserId=5&password="password"
//		//[ActionName("ValidatePassword")]
//		//public IHttpActionResult ValidatePassword(int UserId, string password)
//		//{
//		//	var result = (from a in db.UserTbls
//		//				  where a.UserId == UserId && a.UserPwd == password
//		//				  select new { a.UserName, a.UserId }).ToList();
//		//	if (result.Count > 0)
//		//		return Ok(0);
//		//	else
//		//		return Ok(-1);
//		//}

//		//// GET: api/UserRole/GetUser/5
//		//[HttpGet]
//		//[ActionName("GetUser")]
//		//public IHttpActionResult GetUserTbl(int id)
//		//{
//		//	var result = (from a in db.UserTbls
//		//				  where a.UserId == id
//		//				  select new
//		//				  {
//		//					  a.UserName,
//		//					  a.UserId,
//		//					  a.FirstName,
//		//					  a.LastName,
//		//					  a.UserEmail,
//		//					  a.UserPhone
//		//				  }).ToList();
//		//	UserDataModel userDataModel = new UserDataModel();
//		//	userDataModel.UserId = result.FirstOrDefault().UserId;

//		//	var result1 = (from a in db.UserRoleTbls
//		//				   where a.UserId == id
//		//				   select new { a.RoleId }).ToArray();

//		//	if (result1 != null && result1.Count() > 0)
//		//	{
//		//		userDataModel.RoleId = new int[result1.Count()];
//		//		for (int i = 0; i < result1.Count(); i++)
//		//		{
//		//			userDataModel.RoleId[i] = result1[i].RoleId;
//		//		}
//		//	}
//		//	if (result.Count > 0)
//		//		return Ok(userDataModel);
//		//	else
//		//		return Ok(-1);
//		//}

//		// PUT: api/UserRole/5
//		[ResponseType(typeof(void))]
//		public IHttpActionResult PutUserTbl(int id, UserTbl userTbl)
//		{
//			if (!ModelState.IsValid)
//			{
//				return BadRequest(ModelState);
//			}

//			if (id != userTbl.UserId)
//			{
//				return BadRequest();
//			}

//			db.Entry(userTbl).State = EntityState.Modified;

//			try
//			{
//				db.SaveChanges();
//			}
//			catch (DbUpdateConcurrencyException)
//			{
//				if (!UserTblExists(id))
//				{
//					return NotFound();
//				}
//				else
//				{
//					throw;
//				}
//			}

//			return StatusCode(HttpStatusCode.NoContent);
//		}


//		// POST: api/UserRole
//		[ResponseType(typeof(UserTbl))]
//		public IHttpActionResult PostUserTbl(UserTbl userTbl)
//		{
//			if (!ModelState.IsValid)
//			{
//				return BadRequest(ModelState);
//			}

//			db.UserTbls.Add(userTbl);
//			db.SaveChanges();

//			return CreatedAtRoute("DefaultApi", new { id = userTbl.UserId }, userTbl);
//		}

//		// DELETE: api/UserRole/5
//		[ResponseType(typeof(UserTbl))]
//		public IHttpActionResult DeleteUserTbl(int id)
//		{
//			UserTbl userTbl = db.UserTbls.Find(id);
//			if (userTbl == null)
//			{
//				return NotFound();
//			}

//			db.UserTbls.Remove(userTbl);
//			db.SaveChanges();

//			return Ok(userTbl);
//		}

//		private bool UserTblExists(int id)
//		{
//			return db.UserTbls.Count(e => e.UserId == id) > 0;
//		}
//		//============================================ROLETBL========================================================
//		[HttpGet]
//		[ActionName("GetRole")]
//		public IHttpActionResult GetRole()
//		{
//			var result = (from a in db.RoleTbls
//						  select new { a.RoleId, a.RoleName, a.DeletedDate });
//			return Ok(result);
//		}
//		//=================================================================================================

//		[HttpGet]
//		[ActionName("GetRoleById")]
//		public HttpResponseMessage GetRoleById(int id)
//		{
//			var entity = db.RoleTbls.FirstOrDefault(x => x.RoleId == id);
//			if (entity != null)
//			{
//				return Request.CreateResponse(HttpStatusCode.OK, entity);
//			}
//			else
//			{
//				return Request.CreateErrorResponse(HttpStatusCode.NotFound, "RoleTbl with roleid " +
//					id.ToString() + " not found");
//			}
//		}
//		//===============================================================================================

//		// Post: api/UserRole/AddAccessPermissionTbl
//		[HttpPost]
//		[ActionName("AddAccessPermissionTbl")]
//		public HttpResponseMessage AddAccessPermissionTbl(UserDataModel userDataModel)
//		{
//			int recentUserId = 0;
//			try
//			{
//				AccessPermissionTbl accessPermissionTbl = new AccessPermissionTbl();
//				accessPermissionTbl.UserId = userDataModel.UserId;
//				accessPermissionTbl.AccessId = userDataModel.AccessId;
//				accessPermissionTbl.PermissionId = userDataModel.PermissionId;

//				db.AccessPermissionTbls.Add(accessPermissionTbl);
//				db.SaveChanges();
//				recentUserId = accessPermissionTbl.UserId;
//				var result = (from a in db.AccessPermissionTbls
//							  where a.UserId.Equals(recentUserId)
//							  select new
//							  {
//								  a.UserId,
//								  a.AccessId,
//								  a.PermissionId
//							  }).ToList();

//				var message = Request.CreateResponse(HttpStatusCode.Created, result);
//				return message;
//			}
//			catch (Exception)
//			{
//				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "UserId, AccessId and PermissionId this sequence is already exists");
//			}
//		}
//		//==========================================================================================
//		//// Post: api/UserRole/AddRoleTblAndRoleAccessPermissionTbl
//		//[HttpPost]
//		//[ActionName("AddRoleTblAndRoleAccessPermissionTbl")]
//		//public IHttpActionResult AddRoleTblAndAccessPermissionTbl(UserDataModel userDataModel)
//		//{
//		//	try
//		//	{
//		//		RoleTbl roleTbl = new RoleTbl();
//		//		roleTbl.RoleName = userDataModel.RoleName;

//		//		db.RoleTbls.Add(roleTbl);
//		//		db.SaveChanges();
//		//		int latestRoleId = roleTbl.RoleId;
//		//		int userId = userDataModel.UserId;

//		//		var result = (from a in db.AccessPermissionTbls
//		//					  where a.UserId.Equals(userId)
//		//					  select new
//		//					  {
//		//						  a.AccessId,
//		//						  a.PermissionId,
//		//						  a.UserId
//		//					  }).ToList();

//		//		if (result.Count() > 0)
//		//		{
//		//			for (int i = 0; i < result.Count(); i++)
//		//			{
//		//				RoleAccessPermissionTbl roleAccessPermissionTbl = new RoleAccessPermissionTbl();

//		//				roleAccessPermissionTbl.AccessId = result.ToList()[i].AccessId;
//		//				roleAccessPermissionTbl.PermissionId = result.ToList()[i].PermissionId;
//		//				roleAccessPermissionTbl.RoleId = latestRoleId;
//		//				db.RoleAccessPermissionTbls.Add(roleAccessPermissionTbl);

//		//			}
//		//		}
//		//		if (result.Count() > 0)
//		//		{
//		//			for (int j = 0; j < result.Count(); j++)
//		//			{

//		//				AccessPermissionTbl accessPermissiontbl = new AccessPermissionTbl();

//		//				int userid1 = result.ToList()[j].UserId;
//		//				int access = result.ToList()[j].AccessId;
//		//				int permission = result.ToList()[j].PermissionId;
//		//				AccessPermissionTbl accessPermissionTbl1 = db.AccessPermissionTbls.Find(userid1, access, permission);
//		//				db.AccessPermissionTbls.Remove(accessPermissionTbl1);
//		//			}
//		//		}

//		//		db.SaveChanges();
//		//		return Ok(result);
//		//	}
//		//	catch (Exception ex)
//		//	{
//		//		return Content(HttpStatusCode.BadRequest, ex);
//		//	}

//		//}
//		//=======================================================================================

//		[HttpDelete]
//		[ActionName("DeleteRole")]
//		public HttpResponseMessage DeleteRole(int id)
//		{
//			try
//			{
//				var entity = db.RoleTbls.FirstOrDefault(x => x.RoleId == id);
//				if (entity == null)
//				{
//					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "RoleTbl with roleid " +
//						id.ToString() + " not found");
//				}
//				else
//				{
//					db.RoleTbls.Remove(entity);
//					db.SaveChanges();
//					return Request.CreateResponse(HttpStatusCode.OK, entity);
//				}
//			}
//			catch (Exception ex)
//			{
//				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
//			}
//		}
//		//============================================================================================

//		[HttpPut]
//		[ActionName("UpdateRole")]
//		public HttpResponseMessage UpdateRole(int id, RoleTbl roleTbl)
//		{
//			try
//			{
//				var data = db.RoleTbls.FirstOrDefault(x => x.RoleId == id);
//				if (data == null)
//				{
//					return Request.CreateErrorResponse(HttpStatusCode.NotFound,
//						"UserTbl with userid = " + id.ToString() + " not found to update");
//				}
//				else
//				{
//					data.RoleId = roleTbl.RoleId;
//					data.RoleName = roleTbl.RoleName;
//					db.SaveChanges();
//					return Request.CreateResponse(HttpStatusCode.OK, data);
//				}
//			}
//			catch (Exception ex)
//			{
//				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
//			}
//		}
//		//==================================Permission Tbl==========================================================
//		// GET: api/UserRole/GetPermission
//		//[HttpGet]
//		//[ActionName("GetPermission")]
//		//public IHttpActionResult GetPermission()
//		//{
//		//	var result = (from a in db.PermissionTbls
//		//				  select new { a.PermissionId, a.PermissionName });
//		//	return Ok(result);
//		//}
//		//====================================RoleAccessPermission Tbl===============================
//		//GET: api/UserRole/GetRoleAccessPermission
//		[HttpGet]
//		[ActionName("GetRoleAccessPermission")]
//		public IHttpActionResult GetRoleAccessPermission()
//		{
//			var result = (from a in db.RoleAccessPermissionTbls
//						  select new { a.RoleId, a.AccessId, a.PermissionId });
//			return Ok(result);
//		}
//		//====================================Access Tbl==============================================
//		// GET: api/UserRole/GetAccessTbl
//		//[HttpGet]
//		//[ActionName("GetAccessTbl")]
//		//public IHttpActionResult GetAccessTbl()
//		//{
//		//	var result = (from a in db.AccessTbls
//		//				  select new { a.AccessId, a.AccessName });
//		//	return Ok(result);
//		//}
//		//=====================================================================================

//		//POST: api/UserRole/AddAccessPermissionTblToFetchRoleAccessPermissionTbl
//		[HttpPost]
//		[ActionName("AddAccessPermissionTblToFetchRoleAccessPermissionTbl")]
//		public HttpResponseMessage AddAccessPermissionTblToFetchRoleAccessPermissionTbl(int roleId, UserDataModel userDataModel)
//		{
//			var entity = db.RoleAccessPermissionTbls.FirstOrDefault(x => x.RoleId == roleId);
//			if(entity != null)
//			{
//				return Request.CreateResponse(HttpStatusCode.OK, entity);
//			}
//			else
//			{
//				return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Role with roleid" + roleId.ToString() + "noyt found");         
//			}
//			//try
//			//{
//			//	AccessPermissionTbl accessPermissionTbl = new AccessPermissionTbl();
//			//	RoleAccessPermissionTbl roleAccessPermissionTbl = new RoleAccessPermissionTbl();
//			//	int userId = userDataModel.UserId;
//			//	roleAccessPermissionTbl.RoleId = roleId;
//			//	var entity = db.RoleAccessPermissionTbls.FirstOrDefault(r => r.RoleId == roleId);
//			//	for (int i = 0; i < entity.RoleId; ++i)

//			//		db.SaveChanges();
//			//	var message = Request.CreateResponse(HttpStatusCode.Created);
//			//	return message;
//			//}
//			//catch (Exception ex)
//			//{
//			//	return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
//			//}
//		}

//		[HttpGet]
//		[ActionName("GetOneByOne")]
//		public HttpResponseMessage GetOneByOne(int id)
//		{
//			List<AccessPermissionTbl> accessPermissionTbls = new List<AccessPermissionTbl>();

//			var entity = db.AccessPermissionTbls.FirstOrDefault(a => a.UserId == id);
//			if (entity != null)
//			{
//				return Request.CreateResponse(HttpStatusCode.OK, accessPermissionTbls);
//			}
//			else
//			{
//				return Request.CreateErrorResponse(HttpStatusCode.NotFound, "not found");
//			}
//		}
//	}
//}