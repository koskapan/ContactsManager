using ContactsManager.Domain.Abstract;
using ContactsManager.Domain.Concrete;
using ContactsManager.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData;

namespace ContactsManager.Web.Controllers
{
    [RoutePrefix("api/v1/contacts")]
    public class ContactsController : ApiController
    {
        private IContactRepository repository;
        
        public ContactsController(IContactRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [Route("all")]
        [EnableQuery]
        public IQueryable<Contact> Get()
        {
            return repository.AsQueryable();
        }

        // GET api/values/5
        [HttpGet]
        [Route("{id}")]
        public Contact Get(int id)
        {
            return repository.Get(id);
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
