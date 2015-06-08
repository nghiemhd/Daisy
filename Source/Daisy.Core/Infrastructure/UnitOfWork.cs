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
        private Dictionary<Type, object> repositories;
        private bool disposed;

        public IList<string> Errors { get; set; }

        ~UnitOfWork()
        {
            Dispose(false);
        }

        public UnitOfWork()
        {
            context = new TContext();
            repositories = new Dictionary<Type, object>();
            disposed = false;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (repositories.Keys.Contains(typeof(TEntity)))
            {
                return repositories[typeof(TEntity)] as IRepository<TEntity>;
            }

            var repository = new Repository<TEntity>(context);
            repositories.Add(typeof(TEntity), repository);

            return repository;
        }

        public void Commit()
        {
            try
            {
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                Errors = GetExceptionErrors(ex);
                throw ex;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                this.disposed = true;
            }
        }

        private List<string> GetExceptionErrors(Exception ex)
        {
            var errorList = new List<string>();

            //DbEntityValidationException
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
            //DbUpdateConcurrencyException
            else if (ex is DbUpdateConcurrencyException)
            {
                string customErrorMessage;
                customErrorMessage = "Concurrency violation:" + Environment.NewLine;
                customErrorMessage += "The object you are editing has been changed by another user. Please refresh and update again.";
                errorList.Add(customErrorMessage);
            }
            //DbUpdateException
            else if (ex is DbUpdateException)
            {
                errorList = GetExceptionMessages(ex);
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
                errorList.AddRange(GetExceptionMessages(ex.InnerException));
            }
            return errorList;
        }
    }
}
