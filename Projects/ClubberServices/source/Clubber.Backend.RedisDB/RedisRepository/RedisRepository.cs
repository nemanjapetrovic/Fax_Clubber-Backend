using Clubber.Backend.RedisDB.DependencyInjectionContainer;
using Clubber.Common.Exceptions.Exceptions;
using StackExchange.Redis;
using System;
using System.Text.RegularExpressions;

namespace Clubber.Backend.RedisDB.RedisRepository
{
    public class RedisRepository : IRedisRepository
    {
        private readonly IDatabase _redisDatabase;

        /// <summary>
        /// Creates RedisDatabase reference.
        /// </summary>
        public RedisRepository(string connectionString)
        {

            _redisDatabase = DependencyContainer.Instance.RedisClient(connectionString).GetDatabase();

            if (_redisDatabase == null)
            {
                throw new InternalServerErrorException("Redis database is null!");
            }

            // Validate the connection
            IsConnected();
        }

        /// <summary>
        /// Validation for client connectivity.
        /// </summary>
        private void IsConnected()
        {
            if (!DependencyContainer.Instance.RedisClient().IsConnected)
            {
                throw new InternalServerErrorException("Redis client is not connected!");
            }
        }

        /// <summary>
        /// Used to create a unique key for redis database. Key will be unique because of keyUniqueValue parameter.
        /// </summary>
        /// <param name="keyModel">Type of a entity.</param>
        /// <param name="keyAdditionalInfo">Type of a data that will be stored.</param>
        /// <param name="keyUniqueValue">Unique value for redis keys.</param>
        /// <returns>Generated key.</returns>
        private string KeyCreation(string keyModel, string keyAdditionalInfo, string keyUniqueValue)
        {
            var tmpKeyModel = Regex.Replace(keyModel, @"\s+", "").ToLower();
            var tmpKeyAdditionalInfo = Regex.Replace(keyAdditionalInfo, @"\s+", "").ToLower();
            var tmpKeyUniqueValue = Regex.Replace(keyUniqueValue, @"\s+", "").ToLower();

            return $"{tmpKeyModel}:{tmpKeyAdditionalInfo}:{tmpKeyUniqueValue}";
        }

        /// <summary>
        /// Returns all values from a Set by a key.
        /// </summary>
        /// <param name="keyModel">Type of a entity.</param>
        /// <param name="keyAdditionalInfo">Type of a data that will be stored.</param>
        /// <param name="keyUniqueValue">Unique value for redis keys.</param>
        /// <returns>All values from Redis SETS.</returns>
        public string[] GetSet(string keyModel, string keyAdditionalInfo, string keyUniqueValue)
        {
            var key = KeyCreation(keyModel, keyAdditionalInfo, keyUniqueValue);

            // Get all items from "SMEMBERS"
            var items = _redisDatabase.SetMembers(key);

            return items.ToStringArray();
        }

        /// <summary>
        /// Store a one value in a Set by it's key.
        /// </summary>
        /// <param name="keyModel">Type of a entity.</param>
        /// <param name="keyAdditionalInfo">Type of a data that will be stored.</param>
        /// <param name="keyUniqueValue">Unique value for redis keys.</param>
        /// <param name="storeValue">Value that will be stored.</param>
        /// <returns>If value is stored it will return true, if not will return false.</returns>
        public bool StoreSet(string keyModel, string keyAdditionalInfo, string keyUniqueValue, string storeValue)
        {
            var key = KeyCreation(keyModel, keyAdditionalInfo, keyUniqueValue);

            // Add item to the "SADD"
            _redisDatabase.SetAdd(key, storeValue);

            // Check if value is stored
            return _redisDatabase.SetContains(key, storeValue);
        }

        /// <summary>
        /// Remove one value from Set by it's stored value.
        /// </summary>
        /// <param name="keyModel">Type of a entity.</param>
        /// <param name="keyAdditionalInfo">Type of a data that will be stored.</param>
        /// <param name="keyUniqueValue">Unique value for redis keys.</param>
        /// <param name="storedValue">Stored value in redis, thats already exists.</param>
        /// <returns>Will return true if value is removed, if not will return false.</returns>
        public bool RemoveSet(string keyModel, string keyAdditionalInfo, string keyUniqueValue, string storedValue)
        {
            var key = KeyCreation(keyModel, keyAdditionalInfo, keyUniqueValue);

            // Remove value from the "SREM"
            _redisDatabase.SetRemove(key, storedValue);

            // Return true if the value is removed
            return !_redisDatabase.SetContains(key, storedValue);
        }


        public string GetString(string keyModel, string keyAdditionalInfo, string keyUniqueValue)
        {
            var key = KeyCreation(keyModel, keyAdditionalInfo, keyUniqueValue);

            var item = (string)_redisDatabase.StringGet(key);

            if (string.IsNullOrEmpty(item))
            {
                return null;
            }

            if (item.Equals("nil", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            return item;
        }

        public bool StoreString(string keyModel, string keyAdditionalInfo, string keyUniqueValue, string storeValue)
        {
            var key = KeyCreation(keyModel, keyAdditionalInfo, keyUniqueValue);

            _redisDatabase.StringSet(key, storeValue);

            var item = (string)_redisDatabase.StringGet(key);

            if (string.IsNullOrEmpty(item))
            {
                return false;
            }

            if (item.Equals("nil", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return true;
        }

        public bool RemoveString(string keyModel, string keyAdditionalInfo, string keyUniqueValue)
        {
            var key = KeyCreation(keyModel, keyAdditionalInfo, keyUniqueValue);

            return _redisDatabase.KeyDelete(key);
        }
    }
}
