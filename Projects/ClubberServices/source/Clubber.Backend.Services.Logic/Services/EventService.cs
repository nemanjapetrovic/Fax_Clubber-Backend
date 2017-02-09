using Clubber.Backend.Models.Model;
using Clubber.Backend.MongoDB.MongoManagers;
using System.Linq;
using MongoDB.Bson;
using Clubber.Backend.RedisDB.RedisManagers;
using Clubber.Backend.Services.Logic.Helpers;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Clubber.Backend.Services.Logic.Services
{
    public class EventService : IService<Event>
    {
        private readonly EventMongoManager _mongoEventManager;
        private readonly ClubRedisManager _redisClubManager;

        public EventService(string mongoConStr,
                           string mongoDbName,
                           string redisConStr)
        {
            _mongoEventManager = new EventMongoManager(mongoConStr, mongoDbName);
            _redisClubManager = new ClubRedisManager(redisConStr);
        }

        public void Add(Event entity)
        {
            // Add to MongoDB
            _mongoEventManager.EventRepository.Add(entity);
            // Add _id to RedisDB id cache
            _redisClubManager.ClubRepository.StoreSet(
                Constants.RedisDB.EventEntityName,
                Constants.RedisDB.AdditionalInfoName,
                entity.GetNameWithoutSpaces(),
                entity._id.ToString());
            // Add obj to RedisDB obj cache
            _redisClubManager.ClubRepository.StoreString(
                Constants.RedisDB.EventEntityName,
                Constants.RedisDB.AdditionalInfoId,
                entity._id.ToString(),
                JsonConvert.SerializeObject(entity));
        }

        public IQueryable<Event> Get(string value)
        {
            // Load from RedisDB id cache
            var objs = _redisClubManager.ClubRepository.GetSet(
                Constants.RedisDB.EventEntityName,
                Constants.RedisDB.AdditionalInfoName,
                value);

            List<Event> events = new List<Event>();
            foreach (var item in objs)
            {
                var eventt = _redisClubManager.ClubRepository.GetString(
                    Constants.RedisDB.EventEntityName,
                    Constants.RedisDB.AdditionalInfoId,
                    item);

                // Load then from MongoDB
                if (string.IsNullOrEmpty(eventt))
                {
                    var entity = _mongoEventManager.EventRepository.Get(item);
                    if (entity != null)
                    {
                        events.Add(entity);
                    }
                    // TODO: here we can have the situation where there are elements in RedisDB but they do not exist in MongoDB
                    // make this so when the redis have and MongoDB doesn't remove it from redis.                    
                    continue;
                }
                // Load then from RedisDB obj cache                
                events.Add(JsonConvert.DeserializeObject<Event>(eventt));
            }

            return events.AsQueryable<Event>();
        }

        public void Update(Event entity)
        {
            // Update in MongoDB
            _mongoEventManager.EventRepository.Update(item => item._id, entity._id, entity);
            // Update _id to RedisDB id cache
            _redisClubManager.ClubRepository.StoreSet(
                Constants.RedisDB.EventEntityName,
                Constants.RedisDB.AdditionalInfoName,
                entity.GetNameWithoutSpaces(),
                entity._id.ToString());
            // Update obj to RedisDB obj cache
            _redisClubManager.ClubRepository.StoreString(
                Constants.RedisDB.EventEntityName,
                Constants.RedisDB.AdditionalInfoId,
                entity._id.ToString(),
                JsonConvert.SerializeObject(entity));
        }

        public void Delete(string id)
        {
            // Remove from MongoDB
            var entity = _mongoEventManager.EventRepository.Delete(item => item._id, id);
            // Remove from RedisDB id cache
            _redisClubManager.ClubRepository.RemoveSet(
                Constants.RedisDB.EventEntityName,
                Constants.RedisDB.AdditionalInfoName,
                entity.GetNameWithoutSpaces(),
                entity._id.ToString());
            // Remove from RedisDB obj cache
            _redisClubManager.ClubRepository.RemoveString(
                Constants.RedisDB.EventEntityName,
                Constants.RedisDB.AdditionalInfoId,
                entity._id.ToString());
        }
    }
}