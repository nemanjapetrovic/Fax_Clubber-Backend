using Neo4jClient;
using System;

namespace Clubber.Backend.Neo4jDB.DependencyInjectionContainer
{
    public sealed class DependencyContainer
    {
        #region Singleton
        private static volatile DependencyContainer instance;
        private static object sync = new Object();

        private DependencyContainer() { }

        public static DependencyContainer Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (sync)
                    {
                        if (instance == null)
                        {
                            instance = new DependencyContainer();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion

        private GraphClient _neoGraphClient = null;
        /// <summary>
        /// Returns always the same instance of a Neo4j Graph Client.
        /// The instance is only initialized when the first time is code called.
        /// </summary>
        /// <param name="connectionString">Connection string to the Neo4j database.</param>
        /// <returns>Always the same instance of a Neo4j Graph Client.</returns>
        public GraphClient Neo4jClient(string connectionString = null,
            string username = null, string password = null)
        {
            if (_neoGraphClient == null)
            {
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new Exception("DependencyContainer Neo4j Client connection string is null or empty!");
                }

                _neoGraphClient = new GraphClient(new Uri(connectionString), username, password);
            }
            return _neoGraphClient;
        }

    }
}

