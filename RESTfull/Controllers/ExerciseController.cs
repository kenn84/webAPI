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
    public class ExerciseController : ApiController
    {
        private mHealthDatabaseEntities1 db = new mHealthDatabaseEntities1();

        //// GET: api/Exercise
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        [HttpGet]
        public IHttpActionResult GetExercise(int id)
        {
            Exercise exercise = new Exercise();
            try
            {
                exercise = db.Exercises.ToList().Where((u) => { return u.id == id; }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            if (exercise == null)
            {
                return Ok("Øvelsen findes ikke");
            }
            else
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(exercise);
                return Json(json);
            }
        }

        // POST: api/Exercise
        [HttpPost]
        public HttpResponseMessage AddExercise(string title, string description)
        {
            try
            {
                Exercise exercise = new Exercise { title = title, description = description };
                db.Exercises.Add(exercise);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Accepted, "Brugeren er gemt");

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.ToString());
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdateExercise(int id, string title, string description)
        {
            Exercise exercise = new Exercise();
            var entry = db.Entry<Exercise>(exercise);
            entry.Entity.title = title;
            entry.Entity.description = description;
            entry.State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
            return Request.CreateResponse(HttpStatusCode.Accepted, "En øvelse er ændret");

        }

        [HttpDelete]
        public HttpResponseMessage DeleteExercise(int id)
        {
            try
            {
                db.Exercises.Remove(db.Exercises.Where((u) => u.id == id).FirstOrDefault());
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Accepted, "Øvelsen er fjernet fra systemet");
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.ToString());
            }
        }
    }
}
