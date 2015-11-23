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
using Contoso_Uni.Models;

namespace Contoso_Uni.Controllers
{
    public class EnrollmentsController : ApiController
    {
        private Contoso_UniContext db = new Contoso_UniContext();

        // GET: api/Enrollments
        public IQueryable<Enrollment> GetEnrollment()
        {
            return db.Enrollment;
        }

        // GET: api/Enrollments/5
        [ResponseType(typeof(Enrollment))]
        public IHttpActionResult GetEnrollment(int id)
        {
            Enrollment enrollment = db.Enrollment.Find(id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return Ok(enrollment);
        }

        // PUT: api/Enrollments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEnrollment(int id, Enrollment enrollment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != enrollment.EnrollmentID)
            {
                return BadRequest();
            }

            db.Entry(enrollment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnrollmentExists(id))
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

        // POST: api/Enrollments
        [ResponseType(typeof(Enrollment))]
        public IHttpActionResult PostEnrollment(Enrollment enrollment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Enrollment.Add(enrollment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = enrollment.EnrollmentID }, enrollment);
        }

        // DELETE: api/Enrollments/5
        [ResponseType(typeof(Enrollment))]
        public IHttpActionResult DeleteEnrollment(int id)
        {
            Enrollment enrollment = db.Enrollment.Find(id);
            if (enrollment == null)
            {
                return NotFound();
            }

            db.Enrollment.Remove(enrollment);
            db.SaveChanges();

            return Ok(enrollment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EnrollmentExists(int id)
        {
            return db.Enrollment.Count(e => e.EnrollmentID == id) > 0;
        }
    }
}