using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace yazilimtestdunyasi.Base.Helpers
{
    class Logger
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static Logger instance;

        private Logger() { }
        public static Logger getInstance()
        {//Singleton Tasarım Kalıbı...
            if (instance == null)
                instance = new Logger();
            return instance;
        }

        public void ExceptionLog(string errorMessage, string url, Exception ex)
        {
            string errorStatus = ex.GetType().FullName;
            string exceptionMessage = ex.Message;

            log.Error(string.Format(
                Environment.NewLine + " > URL               => {0} " +
                Environment.NewLine + " > errorMessage      => {1} " +
                Environment.NewLine + " > exceptionMessage  => {2} " +
                Environment.NewLine + " > ERROR DETAIL (ex) => " + Environment.NewLine, url, errorMessage, errorStatus, exceptionMessage), ex);
        }

        public void ExceptionLog(string url, string errorMessage, TestStatus testResult)
        {
            log.Error(string.Format(
                Environment.NewLine + " > URL           => {0} " +
                Environment.NewLine + " > Error Message => {1} " +
                Environment.NewLine + " > Test Result   => {2} " + Environment.NewLine
                , url, errorMessage, testResult));
        }

    }
}

