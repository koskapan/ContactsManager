using ContactsManager.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.Domain.Abstract
{
    public interface IUnitOfWork
    {
        IGenericRepository<IDataEntity> ContactsRepository { get; }
        void SaveChanges();
    }
}
