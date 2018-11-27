using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RESTfull.Models; 

namespace RESTfull.Controllers
{
    public class FeedbackController : ApiController
    {
        mHealthDatabaseEntities db = new mHealthDatabaseEntities();
        

        [HttpPost]
        public HttpResponseMessage AddClient(int height, DateTime date, int weight , int gender)
        {
            
            try
            {
                Client client = new Client();
                client.height = height;
                client.birthdate = date;
                client.weight = weight;
                client.gender = gender;
                db.Clients.Add(client);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Accepted, "Brugeren er gemt"); 

            } catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.ToString());
            }
        }

        [HttpGet]
        public IHttpActionResult GetClient(int id)
        {
            Client client = new Client();
            try
            {
                client = db.Clients.ToList().Where((u) => { return u.ID == id;  }).FirstOrDefault();
            } catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            if (client == null)
            {
                return Ok("Ingen brugere fundet");
            }
            else
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(client);
                return Json(json);
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdateClient(int id , int height , DateTime date, int weight , int gender)
        {
            Client client = new Client();
            var entry = db.Entry<Client>(client);
            entry.Entity.ID = id;
            entry.Entity.height = height;
            entry.Entity.birthdate = date;
            entry.Entity.weight = weight;
            entry.Entity.gender = gender;
            entry.State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
            return Request.CreateResponse(HttpStatusCode.Accepted, "Brugeren er gemt");

            }
        [HttpDelete]
        public HttpResponseMessage DeleteClient(int id)
        {
            try
            {
                db.Clients.Remove(db.Clients.Where((u) => u.ID == id).FirstOrDefault());
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Accepted, "Brugeren er slettet");
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.ToString());
            }
    
        }
    }
}
