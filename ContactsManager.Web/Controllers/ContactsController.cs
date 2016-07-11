using ContactsManager.Domain.Abstract;
using ContactsManager.Domain.Concrete;
using ContactsManager.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Http;

namespace ContactsManager.Web.Controllers
{
    public class ContactsController : ApiController
    {
        private IContactRepository repository;
        
        public ContactsController(IContactRepository repository)
        {
            this.repository = repository;
        }
        
        // GET api/values
        [HttpGet]
        public IEnumerable<Contact> Get()
        {
            return repository.Get();
        }

        // GET api/values/5
        [HttpGet]
        public Contact Get(int id)
        {
            return repository.Get(c => c.Id == id).FirstOrDefault();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Contact value)
        {
            repository.Create(value);
        }

        // PUT api/values/5
        [HttpPut]
        public void Put(int id, [FromBody]Contact value)
        {
            repository.Edit(id, value);
        }

        // DELETE api/values/5
        [HttpDelete]
        public void Delete(int id)
        {
            repository.Delete(id);
        }
    }
}
