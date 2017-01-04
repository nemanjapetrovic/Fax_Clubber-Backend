using ServiceStack.Redis;
using System.Collections.Generic;

namespace Clubber.Backend.RedisDB.RedisRepository
{
    public class RedisRepository : IRedisRepository
    {
        private readonly IRedisClient _redisClient;

        public RedisRepository(string connectionString)
        {
            _redisClient = new RedisClient(connectionString);
        }

        public HashSet<string> Get(string keyModel, string keyAdditionalInfo, string keyUniqueValue)
        {
            //Create key
            var key = $"{keyModel}:{keyAdditionalInfo}:{keyUniqueValue}";

            //Get all items from the key
            return _redisClient.GetAllItemsFromSet(key);
        }

        public bool Store(string keyModel, string keyAdditionalInfo, string keyUniqueValue, string storeValue)
        {
            //Create key
            var key = $"{keyModel}:{keyAdditionalInfo}:{keyUniqueValue}";

            //Count
            long countBefore = _redisClient.GetSetCount(key);
            //Add item
            _redisClient.AddItemToSet(key, storeValue);
            //Count
            long countAfter = _redisClient.GetSetCount(key);

            return (countAfter - countBefore > 0) ? true : false;
        }

        public bool Remove(string keyModel, string keyAdditionalInfo, string keyUniqueValue, string storedValue)
        {
            //Create key
            var key = $"{keyModel}:{keyAdditionalInfo}:{keyUniqueValue}";

            //Count
            long countBefore = _redisClient.GetSetCount(key);
            //Remove item
            _redisClient.RemoveItemFromSet(key, storedValue);
            //Count
            long countAfter = _redisClient.GetSetCount(key);

            return (countBefore - countAfter > 0) ? true : false;
        }
    }
}
