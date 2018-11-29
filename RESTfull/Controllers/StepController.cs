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
        mHealthDatabaseEntities2 db = new mHealthDatabaseEntities2();


        [HttpPost]
        public HttpResponseMessage AddStep(int id, DateTime date, int count)
        {

            try
            {
                Step step = new Step();
                step.id = id;
                step.date = date;
                step.count = count;
              
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
        public IHttpActionResult GetStep(int id)
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
        public HttpResponseMessage UpdateStep(int id, DateTime date, int count)
        {
            Step step = new Step();
            var entry = db.Entry<Step>(step);
            entry.Entity.id= id;
            entry.Entity.date = date;
            entry.Entity.count = count;
       
        

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
        public HttpResponseMessage DeleteStep(int id)
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
