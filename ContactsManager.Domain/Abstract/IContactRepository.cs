using ContactsManager.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.Domain.Abstract
{
    public interface IContactRepository
    {
        IQueryable<Contact> AsQueryable();
        IEnumerable<Contact> Get();
        IEnumerable<Contact> Get(Func<Contact, bool> predicate);
        void Create(Contact entity);
        void Edit(int id, Contact entity);
        void Delete(int id);
    }
}
