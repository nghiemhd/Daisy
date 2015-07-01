using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Core.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T Insert(T entity);
        void MarkAsDeleted(T entity);
        void Delete(T entity);
        void Update(T entity);
        bool Any();
    }
}
