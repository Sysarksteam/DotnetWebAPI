using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using RecpMgmtWebApi.Models;

namespace RecpMgmtWebApi.Controllers
{
    public class AccessPermissionTblsController : ApiController
    {
        private RcpMgmtConnString db = new RcpMgmtConnString();

        // GET: api/AccessPermissionTbls
        public IQueryable<AccessPermissionTbl> GetAccessPermissionTbls()
        {
            return db.AccessPermissionTbls;
        }

        // GET: api/AccessPermissionTbls/5
        [ResponseType(typeof(AccessPermissionTbl))]
        public IHttpActionResult GetAccessPermissionTbl(int id)
        {
            AccessPermissionTbl accessPermissionTbl = db.AccessPermissionTbls.Find(id);
            if (accessPermissionTbl == null)
            {
                return NotFound();
            }

            return Ok(accessPermissionTbl);
        }

        // PUT: api/AccessPermissionTbls/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAccessPermissionTbl(int id, AccessPermissionTbl accessPermissionTbl)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != accessPermissionTbl.UserId)
            {
                return BadRequest();
            }

            db.Entry(accessPermissionTbl).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccessPermissionTblExists(id))
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

        // POST: api/AccessPermissionTbls
        [ResponseType(typeof(AccessPermissionTbl))]
        public IHttpActionResult PostAccessPermissionTbl(AccessPermissionTbl accessPermissionTbl)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AccessPermissionTbls.Add(accessPermissionTbl);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AccessPermissionTblExists(accessPermissionTbl.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = accessPermissionTbl.UserId }, accessPermissionTbl);
        }

        // DELETE: api/AccessPermissionTbls/5
        [ResponseType(typeof(AccessPermissionTbl))]
        public IHttpActionResult DeleteAccessPermissionTbl(int id )
        {
            AccessPermissionTbl accessPermissionTbl = db.AccessPermissionTbls.Find(id);
            if (accessPermissionTbl == null)
            {
                return NotFound();
            }

            db.AccessPermissionTbls.Remove(accessPermissionTbl);
            db.SaveChanges();

            return Ok(accessPermissionTbl);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AccessPermissionTblExists(int id)
        {
            return db.AccessPermissionTbls.Count(e => e.UserId == id) > 0;
        }
    }
}