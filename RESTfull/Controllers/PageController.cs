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
    public class PageController : ApiController
    {
        private mHealthDatabaseEntities1 db = new mHealthDatabaseEntities1();
        // GET: api/Page
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        public IHttpActionResult GetPage(int id)
        {
            Page page = new Page();
            try
            {
                page = db.Pages.ToList().Where((u) => { return u.id == id; }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            if (page == null)
            {
                return Ok("Dagbogens side findes ikke");
            }
            else
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(page);
                return Json(json);
            }
        }

        // POST: api/Exercise
        [HttpPost]
        public HttpResponseMessage AddPage(DateTime date, string description)
        {
            try
            {
                Page page = new Page { date = date, description = description };
                db.Pages.Add(page);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Accepted, "En ny side i dagbogen er blevet tilføjet");

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.ToString());
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdatePage(int id, DateTime date, string description)
        {
            Page page = new Page();
            var entry = db.Entry<Page>(page);
            entry.Entity.description = description;
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
            return Request.CreateResponse(HttpStatusCode.Accepted, "En ændring i siden er blevet lavet");

        }

        public HttpResponseMessage DeletePage(int id)
        {
            try
            {
                db.Pages.Remove(db.Pages.Where((u) => u.id == id).FirstOrDefault());
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Accepted, "En side fra dagbogen er blevet fjernet");
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.ToString());
            }
        }
    }
}
