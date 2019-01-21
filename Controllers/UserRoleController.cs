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
	[EnableCors(origins: "http://mywebclient.azurewebsites.net", headers: "*", methods: "*")]
	public class UserRoleController : ApiController
    {
		private RcpMgmtConnString db = new RcpMgmtConnString();

		// GET: api/UserRole/GetUsers
		[ActionName("GetUsers")]
		public IHttpActionResult GetUserTbls()
		{
			//return Ok(db.UserTbls.Select(x => new UserTbl { UserId = x.UserId, FirstName = x.FirstName }).ToList());
			var result = (from a in db.UserTbls
						  select new { a.FirstName, a.LastName, a.UserName, a.UserId }).ToList();

			return Ok(result);
		}

		// GET: api/UserRole/ValidateUser?userName="username"
		[HttpGet]
		[ActionName("ValidateUser")]
		public IHttpActionResult ValidateUser(string userName)
		{
			//return Ok(db.UserTbls.Select(x => new UserTbl { UserId = x.UserId, FirstName = x.FirstName }).ToList());
			var result = (from a in db.UserTbls
						  where a.UserName == userName
						  select new { a.UserName, a.UserId }).ToList();
			if (result.Count > 0)
				return Ok(result);
			else
				return Ok(-1);
		}

		[HttpGet]
		// GET: api/UserRole/ValidatePassword?id=5, password="password"
		[ActionName("ValidatePassword")]
		public IHttpActionResult ValidatePassword(int UserId, string password)
		{
			var result = (from a in db.UserTbls
						  where a.UserId == UserId && a.UserPwd == password
						  select new { a.UserName, a.UserId }).ToList();
			if (result.Count > 0)
				return Ok(0);
			else
				return Ok(-1);
		}

		// GET: api/UserRole/GetUser/5
		[HttpGet]
		[ActionName("GetUser")]
		public IHttpActionResult GetUserTbl(int id)
		{
			var result = (from a in db.UserTbls
						  where a.UserId == id
						  select new { a.UserName, a.UserId, a.FirstName, a.LastName }).ToList();
			if (result.Count > 0)
				return Ok(result);
			else
				return Ok(-1);
		}

		// PUT: api/UserRole/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutUserTbl(int id, UserTbl userTbl)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != userTbl.UserId)
			{
				return BadRequest();
			}

			db.Entry(userTbl).State = EntityState.Modified;

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!UserTblExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return StatusCode(HttpStatusCode.NoContent);
		}


		// POST: api/UserRole
		[ResponseType(typeof(UserTbl))]
		public IHttpActionResult PostUserTbl(UserTbl userTbl)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			db.UserTbls.Add(userTbl);
			db.SaveChanges();

			return CreatedAtRoute("DefaultApi", new { id = userTbl.UserId }, userTbl);
		}

		// DELETE: api/UserRole/5
		[ResponseType(typeof(UserTbl))]
		public IHttpActionResult DeleteUserTbl(int id)
		{
			UserTbl userTbl = db.UserTbls.Find(id);
			if (userTbl == null)
			{
				return NotFound();
			}

			db.UserTbls.Remove(userTbl);
			db.SaveChanges();

			return Ok(userTbl);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool UserTblExists(int id)
		{
			return db.UserTbls.Count(e => e.UserId == id) > 0;
		}
	}
}