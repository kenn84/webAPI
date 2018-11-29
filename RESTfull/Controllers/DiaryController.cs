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
    public class DiaryController : ApiController
    {
        private mHealthDatabaseEntities2 db = new mHealthDatabaseEntities2();

        // GET: api/Diary
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        public IHttpActionResult GetDiary(int id)
        {
            Diary diary = new Diary();
            try
            {
                diary = db.Diaries.ToList().Where((u) => { return u.id == id; }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            if (diary == null)
            {
                return Ok("Dagbogens findes ikke");
            }
            else
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(diary);
                return Json(json);
            }
        }

        // POST: api/Exercise
        [HttpPost]
        public HttpResponseMessage AddDiary(DateTime date, string title)
        {
            try
            {
                Diary diary = new Diary { date = date, title = title };
                db.Diaries.Add(diary);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Accepted, "En ny dagbog er blevet oprettet");

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.ToString());
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdateDiary(int id, DateTime date, string title)
        {
            Diary diary = new Diary();
            var entry = db.Entry<Diary>(diary);
            entry.Entity.title = title;
            entry.Entity.date = date;
            entry.State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
            return Request.CreateResponse(HttpStatusCode.Accepted, "En ændring i dagbogen er blevet lavet");

        }

        public HttpResponseMessage DeleteDiary(int id)
        {
            try
            {
                db.Diaries.Remove(db.Diaries.Where((u) => u.id == id).FirstOrDefault());
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Accepted, "Dagbogen er blevet fjernet");
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.ToString());
            }
        }
    }
}
