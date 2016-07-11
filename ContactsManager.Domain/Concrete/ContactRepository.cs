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
            return context.Contacts.AsQueryable();
        }

        public void Create(Contact entity)
        {
            context.Contacts.Add(entity);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var contactForDelete = Get(c => c.Id == id).FirstOrDefault();
            if (contactForDelete != null)
            {
                context.Contacts.Remove(contactForDelete);
                context.SaveChanges();
            }
        }

        public void Edit(int id, Contact entity)
        {
            if (entity.Id == id)
            {
                context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
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
