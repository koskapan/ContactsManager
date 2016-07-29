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
        public PageResult<Contact> Get(ODataQueryOptions<Contact> options, string q = null, int ps = 40)
        {
            //return repository.AsQueryable();
            ODataQuerySettings settings = new ODataQuerySettings
            {
                PageSize = ps
            };
            IQueryable queryedRepository = ApplySearchString(repository.AsQueryable(), q);
            IQueryable results = options.ApplyTo(queryedRepository, settings);
            return new PageResult<Contact>(
                results as IEnumerable<Contact>,
                Request.ODataProperties().NextLink,
                Request.ODataProperties().TotalCount
            );
        }

        public static IQueryable ApplySearchString(IQueryable collection, string query)
        {
            IQueryable<Contact> result = (IQueryable<Contact>)collection;
            if (query != null && query != "undefined")
            {
                string[] separatedQueryString = query.Split(' ');
                for (int i = 0; i < separatedQueryString.Length; i++)
                {
                    result = result.Where(c =>
                        c.FirstName.ToLower().Contains(separatedQueryString[i].ToLower()) ||
                        c.LastName.ToLower().Contains(separatedQueryString[i].ToLower()) ||
                        c.JobTitle.ToLower().Contains(separatedQueryString[i].ToLower()) ||
                        c.CompanyName.ToLower().Contains(separatedQueryString[i].ToLower()) ||
                        c.Email.ToLower().Contains(separatedQueryString[i].ToLower()) ||
                        c.Phone.ToLower().Contains(separatedQueryString[i].ToLower())
                    );
                }
            }
            return result;
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
            contactsHub.AddContact(value);
        }

        // PUT api/values/5
        [HttpPut]
        public void Put(int id, [FromBody]Contact value)
        {
            repository.Edit(id, value);
            contactsHub.EditContact(id, value);
        }

        // DELETE api/values/5
        [HttpDelete]
        public void Delete(int id)
        {
            repository.Delete(id);
            contactsHub.RemoveContact(id);
        }
    }
}
