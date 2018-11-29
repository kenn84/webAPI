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
    public class StepController : ApiController
    {
        mHealthDatabaseEntities1 db = new mHealthDatabaseEntities1();


        [HttpPost]
        public HttpResponseMessage AddClient(int id, DateTime date, int count, int clientId)
        {

            try
            {
                Step step = new Step();
                step.id = id;
                step.date = date;
                step.count = count;
                step.clientId = clientId;
                db.Steps.Add(step);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Accepted, "Skridt er gemt");

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.ToString());
            }
        }

        [HttpGet]
        public IHttpActionResult GetClient(int id)
        {
            Step step = new Step();
            try
            {
                step = db.Steps.ToList().Where((u) => { return u.id == id; }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            if (step == null)
            {
                return Ok("Ingen skridt fundet");
            }
            else
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(step);
                return Json(json);
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdateClient(int id, DateTime date, int count, int clientId)
        {
            Step step = new Step();
            var entry = db.Entry<Step>(step);
            entry.Entity.id= id;
            entry.Entity.date = date;
            entry.Entity.count = count;
            entry.Entity.clientId= clientId;
        

            entry.State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
            return Request.CreateResponse(HttpStatusCode.Accepted, "Skridt er gemt");

        }
        [HttpDelete]
        public HttpResponseMessage DeleteClient(int id)
        {
            try
            {
                db.Steps.Remove(db.Steps.Where((u) => u.id == id).FirstOrDefault());
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Accepted, "Skridt er slettet");
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.ToString());
            }

        }
    }
}
