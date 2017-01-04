﻿using ServiceStack.Redis;

namespace Clubber.Backend.RedisDB.RedisRepository
{
    public class RedisRepository : IRedisRepository
    {
        private readonly IRedisClient _redisClient;

        public RedisRepository(string connectionString)
        {
            _redisClient = new RedisClient(connectionString);
        }

        public string Get(string keyModel, string keyAdditionalInfo, string keyUniqueValue)
        {
            var key = $"{keyModel}:{keyAdditionalInfo}:{keyUniqueValue}";
            return _redisClient.Get<string>(key);
        }

        public bool Store(string keyModel, string keyAdditionalInfo, string keyUniqueValue, string storeValue)
        {
            var key = $"{keyModel}:{keyAdditionalInfo}:{keyUniqueValue}";
            return _redisClient.SetValueIfNotExists(key, storeValue);
        }

        public bool Update(string keyModel, string keyAdditionalInfo, string keyUniqueValue, string storeValue)
        {
            var key = $"{keyModel}:{keyAdditionalInfo}:{keyUniqueValue}";
            return _redisClient.SetValueIfExists(key, storeValue);
        }
    }
}