using ContactsManager.Domain.Abstract;
using ContactsManager.Domain.Concrete;
using ContactsManager.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using System.Net.Http;
using System.Web.Http.OData.Extensions;
using ContactsManager.Web.Hubs;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;

namespace ContactsManager.Web.Controllers
{
    public class ContactsController : ApiController
    {
        private IContactRepository repository;
        private ContactsHub contactsHub;
        
        public ContactsController(IContactRepository repository)
        {
            this.repository = repository;
            contactsHub = new ContactsHub();
        }

        [HttpGet]
        [Route("api/contacts/all")]
        //[EnableQuery(PageSize = 40)]
        public PageResult<Contact> Get(ODataQueryOptions<Contact> options)
        {
            //return repository.AsQueryable();
            ODataQuerySettings settings = new ODataQuerySettings
            {
                PageSize = 40
            };
            IQueryable results = options.ApplyTo(repository.AsQueryable(), settings);
            return new PageResult<Contact>(
                results as IEnumerable<Contact>,
                Request.ODataProperties().NextLink,
                Request.ODataProperties().TotalCount
            );
        }

        // GET api/values/5
        [HttpGet]
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
            /*
            var jsonStr = value.ToString();
            var val = JsonConvert.DeserializeObject<Contact>(jsonStr);*/
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
