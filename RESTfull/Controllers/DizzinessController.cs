using RESTfull.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RESTfull.Controllers
{
    public class DizzinessController : ApiController
    {
        mHealthDatabaseEntities1 db = new mHealthDatabaseEntities1();


        [HttpPost]
        public HttpResponseMessage AddDizziness(int id, DateTime date, int level)
        {

            try
            {
                Dizziness dizz = new Dizziness();
                dizz.id = id;
                dizz.date = date;
                dizz.level = level;
                
                db.Dizzinesses.Add(dizz);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Accepted, "Svimmelhed er gemt");

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.ToString());
            }
        }

        [HttpGet]
        public IHttpActionResult GetDizziness(int id)
        {
            Dizziness dizz = new Dizziness();
            try
            {
                dizz = db.Dizzinesses.ToList().Where((u) => { return u.id == id; }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            if (dizz == null)
            {
                return Ok("Ingen svimmelhed");
            }
            else
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(dizz);
                return Json(json);
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdateDizziness(int id, int height, DateTime date, int level)
        {
            Dizziness dizz = new Dizziness();
            var entry = db.Entry<Dizziness>(dizz);
            entry.Entity.id= id;
            entry.Entity.date = date;
            entry.Entity.level = level;
          
           
            entry.State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
            return Request.CreateResponse(HttpStatusCode.Accepted, "Svimmelhed er gemt");

        }
        [HttpDelete]
        public HttpResponseMessage DeleteDizziness(int id)
        {
            try
            {
                db.Dizzinesses.Remove(db.Dizzinesses.Where((u) => u.id == id).FirstOrDefault());
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Accepted, "Svimmelhed er slettet");
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.ToString());
            }

        }
    }
}
