using Clubber.Backend.Log.LogServices;
using System;

namespace Clubber.Backend.Log.DependencyInjectionContainer
{
    public class LoggerDependencyContainer
    {
        #region Singleton
        private static volatile LoggerDependencyContainer instance;
        private static object sync = new Object();

        private LoggerDependencyContainer() { }

        public static LoggerDependencyContainer Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (sync)
                    {
                        if (instance == null)
                        {
                            instance = new LoggerDependencyContainer();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion

        private LogService _logService = null;
        /// <summary>
        /// Returns instance of LogService.
        /// </summary>
        /// <returns>Returns instance of LogService.</returns>
        public LogService Logger()
        {
            if (_logService == null)
            {
                _logService = new LogService();
            }
            return _logService;
        }
    }
}
