using David.Products.Common.Helpers;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace David.Products.Common.Diagnostics
{
    public class ExceptionLogging
    {
        public static readonly Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Genera excepción en BD.
        /// Para configurar se debe cambiar el ConnectionString en NLog.config| La ddl se encuentra en References
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="user"></param>
        /// <param name="ErrorNumber"></param>
        public static string LogException(Exception ex, string user = "Default", string ErrorNumber = "")
        {
            if (string.IsNullOrEmpty(ErrorNumber))
            {
                ErrorNumber = Utility.GetNewNumber();
            }
            MappedDiagnosticsContext.Set("user_id", user);
            MappedDiagnosticsContext.Set("ErrorNumber", ErrorNumber);
            if (ex == null)
                ex = new Exception();

            logger.Error(string.Format(
                "{0}\n\r{1}"
                , ex.Message ?? ""
                , ex.StackTrace ?? ""));
            return ErrorNumber;
        }

        public static void LogInfo(string info, string user = "Default", string ErrorNumber = "")
        {
            if (string.IsNullOrEmpty(ErrorNumber))
            {
                ErrorNumber = Utility.GetNewNumber();
            }

            MappedDiagnosticsContext.Set("user_id", user);
            MappedDiagnosticsContext.Set("ErrorNumber", ErrorNumber);

            logger.Info(string.Format(
                "{0}\n\r"
                , info));
        }

        public static void LogWarn(string info, string user = "Default", string ErrorNumber = "")
        {
            if (string.IsNullOrEmpty(ErrorNumber))
            {
                ErrorNumber = Utility.GetNewNumber();
            }

            MappedDiagnosticsContext.Set("user_id", user);
            MappedDiagnosticsContext.Set("ErrorNumber", ErrorNumber);

            logger.Warn(string.Format(
                "{0}\n\r"
                , info));
        }
    }
}
