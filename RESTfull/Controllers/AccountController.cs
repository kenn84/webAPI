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
    public class AccountController : ApiController
    {
        mHealthDatabaseEntities1 db = new mHealthDatabaseEntities1();


        [HttpPost]
        public HttpResponseMessage AddAccount(string cpr , string password , string salt)
        {

            try
            {
                Account account = new Account();
                account.cpr = cpr;
                account.password = password;
                account.salt = salt;
              
                db.Accounts.Add(account);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Accepted, "Brugeren er gemt");

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.ToString());
            }
        }

        [HttpGet]
        public IHttpActionResult GetAccount(int id)
        {
            Account account = new Account();
            try
            {
                account= db.Accounts.ToList().Where((u) => { return u.id == id; }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            if (account == null)
            {
                return Ok("Ingen brugere fundet");
            }
            else
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(account);
                return Json(json);
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdateAccount(int id , string cpr , string password , string salt)
        {
            Account account = new Account();
            var entry = db.Entry<Account>(account);
            entry.Entity.id = id;
            entry.Entity.cpr = cpr;
            entry.Entity.password = password;
            entry.Entity.salt = salt;
       
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
        public HttpResponseMessage DeleteAccount(int id)
        {
            try
            {
                db.Accounts.Remove(db.Accounts.Where((u) => u.id == id).FirstOrDefault());
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
