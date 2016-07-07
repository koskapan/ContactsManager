using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.Domain.Abstract
{
    public interface IGenericRepository<T> where T : class, IDataEntity
    {
        IQueryable<T> AsQueryable();
        IEnumerable<T> Get();
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
        IEnumerable<T> Get(int page_num, int page_size);
        T Get(int id);
        void Create(T entity);
        void Edit(T entity);
        void Delete(T entity);
    }
}
