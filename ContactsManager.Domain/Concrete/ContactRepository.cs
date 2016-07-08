using ContactsManager.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsManager.Domain.Entity;

namespace ContactsManager.Domain.Concrete
{
    public class ContactRepository : IContactRepository
    {
        EfDbContext context;

        public ContactRepository()
        {
            context = new EfDbContext();
        }

        public IQueryable<Contact> AsQueryable()
        {
            throw new NotImplementedException();
        }

        public void Create(Contact entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Contact entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(Contact entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Contact> Get()
        {
            return context.Contacts;
        }

        public IEnumerable<Contact> Get(Func<Contact, bool> predicate)
        {
            return context.Contacts.Where(predicate);
        }
    }
}
