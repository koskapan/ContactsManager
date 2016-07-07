using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.Domain.Abstract
{
    public interface IObjectContext: IDisposable
    {
        IObjectSet<TEntity> CreateObjectSet<TEntity>() where TEntity : class;
        void SaveChanges();
    }
}
