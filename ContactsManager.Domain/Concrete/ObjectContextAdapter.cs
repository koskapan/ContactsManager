using ContactsManager.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.Objects;

namespace ContactsManager.Domain.Concrete
{
    public class ObjectContextAdapter : IObjectContext
    {
        private readonly ObjectContext context;

        public ObjectContextAdapter(ObjectContext context)
        {
            this.context = context;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public IObjectSet<TEntity> CreateObjectSet<TEntity>() where TEntity : class, IDataEntity
        {
            return context.CreateObjectSet<TEntity>();
        }
    }
}
