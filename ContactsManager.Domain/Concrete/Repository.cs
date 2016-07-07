using ContactsManager.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.Entity.Core.Objects;

namespace ContactsManager.Domain.Concrete
{
    public class Repository<T> : IGenericRepository<T> where T : class
    {
        private IObjectSet<T> objectSet;

        public Repository(IObjectContext context)
        {
            objectSet = context.CreateObjectSet<T>();
        }

        public IQueryable<T> AsQueryable()
        {
            return objectSet.AsQueryable();
        }

        public void Create(T entity)
        {
            objectSet.AddObject(entity);
        }

        public void Delete(T entity)
        {
            objectSet.DeleteObject(entity);
        }

        public void Edit(T entity)
        {
            objectSet.Attach(entity);
        }

        public IEnumerable<T> Get()
        {
            return objectSet.ToList();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return objectSet.Where(predicate);
        }

        public IEnumerable<T> Get(int page_num, int page_size)
        {
            return objectSet.Skip((page_num - 1) * page_size).Take(page_size);
        }
    }
}
