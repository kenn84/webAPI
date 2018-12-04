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
        public HttpResponseMessage AddAccount(long cpr , string password , string salt)
        {

            try
            {
                Account account = new Account();
                account.cpr = cpr;
                account.password = password;
                account.salt = salt;
              
                db.Accounts.Add(account);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Accepted, account.cpr);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.ToString());
            }
        }

        [HttpGet]
        public IHttpActionResult GetAccount(long cpr)
        {
            Account account = new Account();
            try
            {
                account= db.Accounts.ToList().Where((u) => { return u.cpr == cpr; }).FirstOrDefault();
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
        public HttpResponseMessage UpdateAccount( long cpr , string password , string salt)
        {
            Account account = new Account();
            var entry = db.Entry<Account>(account);
           
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
        public HttpResponseMessage DeleteAccount(long cpr)
        {
            try
            {
                db.Accounts.Remove(db.Accounts.Where((u) => u.cpr == cpr).FirstOrDefault());
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
