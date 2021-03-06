﻿using RESTfull.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RESTfull.Controllers
{
    public class UserController : ApiController
    {
        mHealthDatabaseEntities1 db = new mHealthDatabaseEntities1();
        

        [HttpPost]
        public HttpResponseMessage AddUser(string username, string password, string salt , int height , DateTime birthdate, int weight , int gender)
        {
            bool userAlreadyExists = db.Users.Any(x => x.username == username);
            if (userAlreadyExists != true)
            {
                try
            {
                    User user = new User();
                    user.username = username;
                    user.password = password;
                    user.salt = salt;
                    user.height = height;
                    user.weight = weight;
                    user.birthdate = birthdate;
                    user.gender = gender;
                    db.Users.Add(user);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.Accepted, "Brugeren er oprettet");
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.ToString());
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, "Der findes allerede en med det brugernavn");
            }
        }

        [HttpGet]
        public IHttpActionResult GetUser(string username)
        {
            User user = new User();
            try
            {
                user = db.Users.ToList().Where((u) => u.username == username).FirstOrDefault();
                
           
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            if (user == null)
            {
                return Ok("Ingen brugere fundet");
            }
            else
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(user);
                return Json(json);
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdateUser(string username, string password, string salt, int height, int weight, DateTime birthdate, int gender)
        {
            User user = new User();
            var entry = db.Entry<User>(user);

            entry.Entity.username = username;
            entry.Entity.password = password;
            entry.Entity.salt = salt;
            entry.Entity.height = height;
            entry.Entity.weight = weight;
            entry.Entity.birthdate = birthdate;
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
        public HttpResponseMessage DeleteUser(string username)
        {
            try
            {
                db.Users.Remove(db.Users.Where((u) => u.username == username).FirstOrDefault());
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
