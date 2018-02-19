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
using RadianSampleTask;

namespace RadianSampleTask.Controllers
{
    public class countriesController : ApiController
    {
        private UsersRegistrationEntities db = new UsersRegistrationEntities();

        // GET: api/countries
        public IQueryable<country> Getcountries()
        {
            return db.countries;
        }

        // GET: api/countries/5
        [ResponseType(typeof(country))]
        public IHttpActionResult Getcountry(int id)
        {
            country country = db.countries.Find(id);
            if (country == null)
            {
                return NotFound();
            }

            return Ok(country);
        }

        // PUT: api/countries/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcountry(int id, country country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != country.countryId)
            {
                return BadRequest();
            }

            db.Entry(country).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!countryExists(id))
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

        // POST: api/countries
        [ResponseType(typeof(country))]
        public IHttpActionResult Postcountry(country country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.countries.Add(country);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = country.countryId }, country);
        }

        // DELETE: api/countries/5
        [ResponseType(typeof(country))]
        public IHttpActionResult Deletecountry(int id)
        {
            country country = db.countries.Find(id);
            if (country == null)
            {
                return NotFound();
            }

            db.countries.Remove(country);
            db.SaveChanges();

            return Ok(country);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool countryExists(int id)
        {
            return db.countries.Count(e => e.countryId == id) > 0;
        }
    }
}