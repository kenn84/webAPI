using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RESTfull.Controllers
{
    public class ExerciseController : ApiController
    {
        // GET: api/Exercise
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Exercise/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Exercise
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Exercise/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Exercise/5
        public void Delete(int id)
        {
        }
    }
}
