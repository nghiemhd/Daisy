using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Daisy.Logging.Extensions;

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
            logger.Info(message);
        }

        public void Warn(string message)
        {
            logger.Warn(message);
        }

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Error(string message)
        {
            logger.Error(message);
        }

        public void Error(string message, Exception ex)
        {
            logger.Error(message, ex);
        }

        public void Error(Exception ex)
        {
            logger.Error(ex.BuildExceptionMessage());
        }

        public void Fatal(string message)
        {
            logger.Fatal(message);
        }

        public void Fatal(Exception ex)
        {
            logger.Fatal(ex.BuildExceptionMessage());
        }
    }
}
