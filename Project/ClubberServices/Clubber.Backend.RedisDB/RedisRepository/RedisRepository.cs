using ServiceStack.Redis;
using System.Collections.Generic;

namespace Clubber.Backend.RedisDB.RedisRepository
{
    public class RedisRepository : IRedisRepository
    {
        private readonly IRedisClient _redisClient;

        /// <summary>
        /// Creates RedisClient.
        /// </summary>
        public RedisRepository(string connectionString)
        {
            _redisClient = new RedisClient(connectionString);

            if (_redisClient == null)
            {
                throw new System.Exception("Redis client is null!");
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
            return $"{keyModel}:{keyAdditionalInfo}:{keyUniqueValue}";
        }

        /// <summary>
        /// Returns all values from a Set by a key.
        /// </summary>
        /// <param name="keyModel">Type of a entity.</param>
        /// <param name="keyAdditionalInfo">Type of a data that will be stored.</param>
        /// <param name="keyUniqueValue">Unique value for redis keys.</param>
        /// <returns>All values from Redis SETS.</returns>
        public HashSet<string> Get(string keyModel, string keyAdditionalInfo, string keyUniqueValue)
        {
            var key = KeyCreation(keyModel, keyAdditionalInfo, keyUniqueValue);

            return _redisClient.GetAllItemsFromSet(key);
        }

        /// <summary>
        /// Store a one value in a Set by it's key.
        /// </summary>
        /// <param name="keyModel">Type of a entity.</param>
        /// <param name="keyAdditionalInfo">Type of a data that will be stored.</param>
        /// <param name="keyUniqueValue">Unique value for redis keys.</param>
        /// <param name="storeValue">Value that will be stored.</param>
        /// <returns>If value is stored it will return true, if not will return false.</returns>
        public bool Store(string keyModel, string keyAdditionalInfo, string keyUniqueValue, string storeValue)
        {
            var key = KeyCreation(keyModel, keyAdditionalInfo, keyUniqueValue);

            // Count before adding
            long countBefore = _redisClient.GetSetCount(key);
            // Add item
            _redisClient.AddItemToSet(key, storeValue);
            // Count after adding a new value
            long countAfter = _redisClient.GetSetCount(key);

            // Check if item is stored properly in redis
            return (countAfter - countBefore > 0) ? true : false;
        }

        /// <summary>
        /// Remove one value from Set by it's stored value.
        /// </summary>
        /// <param name="keyModel">Type of a entity.</param>
        /// <param name="keyAdditionalInfo">Type of a data that will be stored.</param>
        /// <param name="keyUniqueValue">Unique value for redis keys.</param>
        /// <param name="storedValue">Stored value in redis, thats already exists.</param>
        /// <returns>Will return true if value is removed, if not will return false.</returns>
        public bool Remove(string keyModel, string keyAdditionalInfo, string keyUniqueValue, string storedValue)
        {
            var key = KeyCreation(keyModel, keyAdditionalInfo, keyUniqueValue);

            // Count before
            long countBefore = _redisClient.GetSetCount(key);
            // Remove item
            _redisClient.RemoveItemFromSet(key, storedValue);
            // Count after removing
            long countAfter = _redisClient.GetSetCount(key);

            // Check if it's properly removed
            return (countBefore - countAfter > 0) ? true : false;
        }
    }
}
