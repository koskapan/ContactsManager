using System;
using System.Collections.Generic;
using System.Web.Http;

namespace ContactsManager.Web.Controllers
{
    [RoutePrefix("api/v1/contacts")]
    public class ContactsController : ApiController
    {
        // GET api/values
        [HttpGet]
        [Route("all/{page_num=1}")]
        public IEnumerable<string> Get(int page_num = 1, int page_size = 1)
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete]
        public void Delete(int id)
        {
        }
    }
}
