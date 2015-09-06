using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Core.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Query();
        T Insert(T entity);
        void MarkAsDeleted(T entity);
        void Delete(T entity);
        void Update(T entity);
        bool Any();
        void RemoveRange(IEnumerable<T> entities);
    }
}
