using ContactsManager.Domain.Abstract;
using ContactsManager.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ContactsManager.Web.Controllers
{
    [RoutePrefix("api/v1/contacts")]
    public class ContactsController : ApiController
    {
        private IUnitOfWork unitOfWork;

        public ContactsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET api/values
        [HttpGet]
        [Route("all/{page_num=1}")]
        public IEnumerable<Contact> Get(int page_num = 1, int page_size = 1)
        {
            return unitOfWork.ContactsRepository.Get(page_num, page_size);
        }

        // GET api/values/5
        [HttpGet]
        public Contact Get(int id)
        {
            return unitOfWork.ContactsRepository.Get(c => c.Id == id).FirstOrDefault();
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
