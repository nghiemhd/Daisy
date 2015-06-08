using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Logging
{
    public class Logger : ILogger
    {
        private ILog logger;

        public Logger(string loggerName)
        {
            log4net.GlobalContext.Properties["CurrentDate"] = DateTime.UtcNow.ToString("yyyy-MM-dd");
            logger = LogManager.GetLogger(loggerName);
        }

        public void Info(string message)
        {
            throw new NotImplementedException();
        }

        public void Warn(string message)
        {
            throw new NotImplementedException();
        }

        public void Debug(string message)
        {
            throw new NotImplementedException();
        }

        public void Error(string message)
        {
            throw new NotImplementedException();
        }

        public void Error(string message, Exception ex)
        {
            throw new NotImplementedException();
        }

        public void Error(Exception ex)
        {
            throw new NotImplementedException();
        }

        public void Fatal(string message)
        {
            throw new NotImplementedException();
        }

        public void Fatal(Exception ex)
        {
            throw new NotImplementedException();
        }
    }
}
