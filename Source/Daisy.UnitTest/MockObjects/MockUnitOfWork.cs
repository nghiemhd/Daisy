using Daisy.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.UnitTest.MockObjects
{
    public class MockUnitOfWork<T> : IUnitOfWork where T : class, new()
    {
        private T context;
        private Dictionary<Type, object> repositories;

        public MockUnitOfWork()
        {
            context = new T();
            repositories = new Dictionary<Type, object>();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (repositories.Keys.Contains(typeof(TEntity)))
            {
                return repositories[typeof(TEntity)] as IRepository<TEntity>;
            }

            var entityName = typeof(TEntity).Name;
            var property = context.GetType().GetProperty(entityName);
            MockRepository<TEntity> repository = null;
            if (property != null)
            {
                var entityValue = property.GetValue(context, null);
                repository = new MockRepository<TEntity>(entityValue as List<TEntity>);
            }
            else
            {
                repository = new MockRepository<TEntity>(new List<TEntity>());
            }

            repositories.Add(typeof(TEntity), repository);
            return repository;
        }

        public void SetRepositoryData<TEntity>(List<TEntity> data) where TEntity : class
        {
            IRepository<TEntity> repository = GetRepository<TEntity>();

            var mockRepository = repository as MockRepository<TEntity>;
            if (mockRepository != null)
            {
                mockRepository.Context = data;
            }
        }

        public void Commit()
        {
            
        }

        public void Dispose()
        {
            
        }
    }
}
