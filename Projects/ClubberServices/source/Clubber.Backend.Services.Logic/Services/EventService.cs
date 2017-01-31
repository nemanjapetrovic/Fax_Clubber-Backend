using Clubber.Backend.Models.Model;
using Clubber.Backend.MongoDB.MongoManagers;
using System.Linq;
using MongoDB.Bson;
using Clubber.Backend.RedisDB.RedisManagers;
using Clubber.Backend.Services.Logic.Helpers;
using System.Collections.Generic;

namespace Clubber.Backend.MongoDB.MongoServices
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
            //Add to MongoDB
            _mongoEventManager.EventRepository.Add(entity);
            //Add _id to RedisDB Sets
            _redisClubManager.ClubRepository.Store(
                Constants.RedisDB.EventEntityName,
                Constants.RedisDB.AdditionalInfoName,
                entity.GetNameWithoutSpaces(),
                entity._id.ToString());
        }

        public IQueryable<Event> Get(string value)
        {
            //Load from RedisDB
            var objs = _redisClubManager.ClubRepository.Get(
                Constants.RedisDB.EventEntityName,
                Constants.RedisDB.AdditionalInfoName,
                value);

            //Load from MongoDB
            List<Event> events = new List<Event>();
            foreach (var item in objs)
            {
                events.Add(_mongoEventManager.EventRepository.Get(new ObjectId(item)));
            }

            return events.AsQueryable<Event>();
        }

        public void Update(Event entity)
        {
            //Only update in MongoDB
            _mongoEventManager.EventRepository.Update(item => item._id, entity._id, entity);
        }

        public void Delete(string id)
        {
            //Remove from MongoDB
            var entity = _mongoEventManager.EventRepository.Delete(item => item._id, new ObjectId(id));
            //Remove from RedisDB
            _redisClubManager.ClubRepository.Remove(
                Constants.RedisDB.EventEntityName,
                Constants.RedisDB.AdditionalInfoName,
                entity.GetNameWithoutSpaces(),
                entity._id.ToString());
        }
    }
}