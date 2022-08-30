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
using IteratesAssessment.DAL;
using IteratesAssessment.Models;

namespace IteratesAssessment.Controllers
{
    public class WholesalersController : ApiController
    {
        private ManagementSystemContext db = new ManagementSystemContext();

        // GET: api/Wholesalers
        public IQueryable<Wholesaler> GetWholesalers()
        {
            return db.Wholesalers;
        }

        // GET: api/Wholesalers/5
        [ResponseType(typeof(Wholesaler))]
        public IHttpActionResult GetWholesaler(int id)
        {
            Wholesaler wholesaler = db.Wholesalers.Find(id);
            if (wholesaler == null)
            {
                return NotFound();
            }

            return Ok(wholesaler);
        }

        // PUT: api/Wholesalers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWholesaler(int id, Wholesaler wholesaler)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != wholesaler.ID)
            {
                return BadRequest();
            }

            db.Entry(wholesaler).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WholesalerExists(id))
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

        // POST: api/Wholesalers
        [ResponseType(typeof(Wholesaler))]
        public IHttpActionResult PostWholesaler(Wholesaler wholesaler)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Wholesalers.Add(wholesaler);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = wholesaler.ID }, wholesaler);
        }

        // DELETE: api/Wholesalers/5
        [ResponseType(typeof(Wholesaler))]
        public IHttpActionResult DeleteWholesaler(int id)
        {
            Wholesaler wholesaler = db.Wholesalers.Find(id);
            if (wholesaler == null)
            {
                return NotFound();
            }

            db.Wholesalers.Remove(wholesaler);
            db.SaveChanges();

            return Ok(wholesaler);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WholesalerExists(int id)
        {
            return db.Wholesalers.Count(e => e.ID == id) > 0;
        }
    }
}