using Clubber.Common.Exceptions.Exceptions;
using StackExchange.Redis;
using System;

namespace Clubber.Backend.RedisDB.DependencyInjectionContainer
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


        private ConnectionMultiplexer _redisClient = null;
        /// <summary>
        /// Returns always the same instance of a Redis Client.
        /// The instance is only initialized when the first time is code called.
        /// </summary>
        /// <param name="connectionString">Connection string to the Redis database.</param>
        /// <returns>Always the same instance of a Redis Client.</returns>
        public ConnectionMultiplexer RedisClient(string connectionString = null)
        {
            if (_redisClient == null)
            {
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InternalServerErrorException("DependencyContainer Redis Client connection string is null or empty!");
                }

                _redisClient = ConnectionMultiplexer.Connect(connectionString);
            }
            return _redisClient;
        }

    }
}

