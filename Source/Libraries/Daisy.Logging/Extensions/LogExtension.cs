using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Logging.Extensions
{
    public static class LogExtension
    {
        public static string BuildExceptionMessage(this Exception ex)
        {
            StringBuilder message = new StringBuilder();
            message.Append(BuildMessage(ex));

            if (ex.InnerException != null)
            {
                var innerException = GetFinalInnerException(ex.InnerException);
                message.Append(Environment.NewLine);
                message.Append("Inner Exception");
                message.Append(BuildMessage(innerException));
            }

            return message.ToString();
        }

        private static string BuildMessage(Exception ex)
        {
            StringBuilder errorMessage = new StringBuilder();

            errorMessage.Append(Environment.NewLine);
            errorMessage.Append("Message :");
            errorMessage.Append(ex.Message);

            errorMessage.Append(Environment.NewLine);
            errorMessage.Append("Source :");
            errorMessage.Append(ex.Source);

            errorMessage.Append(Environment.NewLine);
            errorMessage.Append("Stack Trace :");
            errorMessage.Append(ex.StackTrace);

            errorMessage.Append(Environment.NewLine);
            errorMessage.Append("TargetSite :");
            errorMessage.Append(ex.TargetSite);

            return errorMessage.ToString();
        }

        public static Exception GetFinalInnerException(Exception ex)
        {
            Exception result = ex;
            if (ex.InnerException != null)
            {
                result = GetFinalInnerException(ex.InnerException);
            }
            return result;                        
        }
    }
}
