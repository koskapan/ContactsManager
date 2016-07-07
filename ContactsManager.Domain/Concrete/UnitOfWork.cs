using ContactsManager.Domain.Abstract;
using ContactsManager.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.Domain.Concrete
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IObjectContext objectContext;

        private IGenericRepository<Contact> contactsRepository;

        IGenericRepository<Contact> ContactsRepository
        {
            get
            {
                if (contactsRepository == null)
                    contactsRepository = new Repository<Contact>(objectContext);
                return contactsRepository;
            }
        }

        public UnitOfWork(IObjectContext context)
        {
            objectContext = context;
        }

        public void SaveChanges()
        {
            objectContext.SaveChanges();
        }

        public void Dispose()
        {
            if (objectContext != null)
            {
                objectContext.Dispose();
            }
            GC.SuppressFinalize(this);
        }
    }
}
