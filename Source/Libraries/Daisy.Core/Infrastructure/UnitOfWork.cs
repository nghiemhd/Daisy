using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Core.Infrastructure
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : IDbContext, new()
    {
        private readonly IDbContext context;
        private readonly IUnityContainer container;
        private Dictionary<Type, object> repositories;
        private bool disposed;

        public IList<string> Errors { get; set; }

        public IDbContext DbContext
        {
            get
            {
                return context;
            }
        }

        ~UnitOfWork()
        {
            this.Dispose(false);
        }

        public UnitOfWork(IUnityContainer container)
        {
            this.container = container;
            this.context = this.container.Resolve<TContext>();
            this.repositories = new Dictionary<Type, object>();
            this.disposed = false;
        }        

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (this.repositories.Keys.Contains(typeof(TEntity)))
            {
                return this.repositories[typeof(TEntity)] as IRepository<TEntity>;
            }

            var repository = new Repository<TEntity>(this.context);
            this.repositories.Add(typeof(TEntity), repository);

            return repository;
        }

        public void Commit()
        {
            try
            {
                this.context.SaveChanges();
            }
            catch (Exception ex)
            {
                this.Errors = this.GetExceptionErrors(ex);
                throw ex;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.context.Dispose();
                }
                this.disposed = true;
            }
        }

        private List<string> GetExceptionErrors(Exception ex)
        {
            var errorList = new List<string>();

            // DbEntityValidationException
            if (ex is DbEntityValidationException)
            {
                foreach (var validationResult in ((DbEntityValidationException)ex).EntityValidationErrors)
                {
                    foreach (var error in validationResult.ValidationErrors)
                    {
                        errorList.Add(error.ErrorMessage);
                    }
                }
            }
            // DbUpdateConcurrencyException
            else if (ex is DbUpdateConcurrencyException)
            {
                string customErrorMessage;
                customErrorMessage = "Concurrency violation:" + Environment.NewLine;
                customErrorMessage += "The object you are editing has been changed by another user. Please refresh and update again.";
                errorList.Add(customErrorMessage);
            }
            // DbUpdateException
            else if (ex is DbUpdateException)
            {
                errorList = this.GetExceptionMessages(ex);
            }
            else
            {
                errorList.Add(ex.Message);
            }
            return errorList;
        }

        private List<string> GetExceptionMessages(Exception ex)
        {
            var errorList = new List<string>();
            errorList.Add(ex.Message);
            if (ex.InnerException != null)
            {
                errorList.AddRange(this.GetExceptionMessages(ex.InnerException));
            }
            return errorList;
        }
    }
}
