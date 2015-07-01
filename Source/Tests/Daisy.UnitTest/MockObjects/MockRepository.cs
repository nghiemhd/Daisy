using Daisy.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.UnitTest.MockObjects
{
    public class MockRepository<T> : IRepository<T> where T : class
    {
        public List<T> Context;

        public MockRepository(List<T> context)
        {
            this.Context = context;
        }

        public IQueryable<T> GetAll()
        {
            return Context.AsQueryable();
        }

        public T Insert(T entity)
        {
            Context.Add(entity);
            return entity;
        }

        public void MarkAsDeleted(T entity)
        {
            var baseEntity = entity as BaseEntity;
            if (baseEntity != null)
            {
                baseEntity.IsDeleted = true;
            }
        }

        public void Delete(T entity)
        {
            Context.Remove(entity);
        }

        public void Update(T entity)
        {
            var entry = Context.Where(x => x == entity).SingleOrDefault();
            entry = entity;
        }

        public bool Any()
        {
            return Context.Any();
        }
    }
}
