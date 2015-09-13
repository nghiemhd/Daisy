using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Daisy.Core.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IDbContext context;
        private readonly IDbSet<T> dbSet;

        public Repository(IDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public DbContext EFContext 
        {
            get
            {
                return this.context as DbContext;
            }
        }

        public virtual IQueryable<T> Query()
        {
            return this.dbSet;
        }

        public virtual T Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            BaseEntity baseEntity = entity as BaseEntity;
            if (baseEntity != null)
            {
                baseEntity.IsDeleted = false;
                baseEntity.UpdatedDate = DateTime.Now;
                baseEntity.CreatedDate = DateTime.Now;
                if (string.IsNullOrEmpty(baseEntity.CreatedBy))
                {
                    baseEntity.CreatedBy = Thread.CurrentPrincipal.Identity.Name;
                }
                baseEntity.UpdatedBy = baseEntity.CreatedBy;
                return this.dbSet.Add(baseEntity as T);
            }
            else
            {
                return this.dbSet.Add(entity);
            }
        }

        public virtual void MarkAsDeleted(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            BaseEntity baseEntity = entity as BaseEntity;
            if (baseEntity != null)
            {
                baseEntity.IsDeleted = true;
                Update(baseEntity as T);
            }
        }

        public virtual void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            this.dbSet.Remove(entity);                        
        }

        public virtual void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            BaseEntity baseEntity = entity as BaseEntity;
            if (baseEntity != null)
            {
                baseEntity.UpdatedDate = DateTime.Now;
                var dbEntityEntry = this.context.Entry(baseEntity);

                // Set RowVersion equal to value of client so that
                // entity framework handles concurrency violation
                dbEntityEntry.Property(p => p.RowRevision).OriginalValue = baseEntity.RowRevision;

                // Do not update CreatedBy and CreatedDate
                dbEntityEntry.Property(p => p.CreatedBy).CurrentValue = dbEntityEntry.Property(p => p.CreatedBy).OriginalValue;
                dbEntityEntry.Property(p => p.CreatedDate).CurrentValue = dbEntityEntry.Property(p => p.CreatedDate).OriginalValue;
            }
        }

        public virtual bool Any()
        {
            return this.dbSet.Any();
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }
            this.EFContext.Set<T>().RemoveRange(entities);
        }
    }
}
