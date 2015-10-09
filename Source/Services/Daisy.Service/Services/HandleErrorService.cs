using Daisy.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Service
{
    public abstract class HandleErrorService
    {
        private readonly ILogger logger;

        protected HandleErrorService(ILogger logger)
        {
            this.logger = logger;
        }

        public void Process(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }

        public T Process<T>(Func<T> func)
        {
            try
            {
                var result = func();
                return result;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }
    }
}
