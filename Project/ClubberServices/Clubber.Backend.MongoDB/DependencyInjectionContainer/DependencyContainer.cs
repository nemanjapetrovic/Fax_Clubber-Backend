using MongoDB.Driver;
using System;

namespace Clubber.Backend.MongoDB.DependencyInjectionContainer
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

        private MongoClient _mongoClient = null;
        /// <summary>
        /// Returns always the same instance of a MongoClient.
        /// The instance is only initialized when the first time is code called.
        /// </summary>
        /// <param name="connectionString">Connection string to the Mongo database.</param>
        /// <returns>Always the same instance of a MongoClient.</returns>
        public MongoClient MongoClient(string connectionString = null)
        {
            if (_mongoClient == null)
            {
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new Exception("DependencyContainer MongoClient connection string is null or empty!");
                }

                _mongoClient = new MongoClient(connectionString);
            }
            return _mongoClient;
        }

    }
}
