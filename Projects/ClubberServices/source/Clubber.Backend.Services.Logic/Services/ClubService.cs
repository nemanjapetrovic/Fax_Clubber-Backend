using System.Linq;
using Clubber.Backend.Models.Model;
using Clubber.Backend.MongoDB.MongoManagers;
using Clubber.Backend.Services.Logic.Helpers;
using Clubber.Backend.RedisDB.RedisManagers;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Clubber.Backend.Services.Logic.Services
{
    public class ClubService : IService<Club>
    {
        private readonly ClubMongoManager _mongoClubManager;
        private readonly ClubRedisManager _redisClubManager;

        public ClubService(string mongoConStr,
                           string mongoDbName,
                           string redisConStr)
        {
            _mongoClubManager = new ClubMongoManager(mongoConStr, mongoDbName);
            _redisClubManager = new ClubRedisManager(redisConStr);
        }

        public void Add(Club entity)
        {
            // Add to MongoDB
            _mongoClubManager.ClubRepository.Add(entity);
            // Add _id to RedisDB id cache
            _redisClubManager.ClubRepository.StoreSet(
                Constants.RedisDB.ClubEntityName,
                Constants.RedisDB.AdditionalInfoName,
                entity.GetNameWithoutSpaces(),
                entity._id.ToString());
            // Add obj to RedisDB obj cache
            _redisClubManager.ClubRepository.StoreString(
                Constants.RedisDB.ClubEntityName,
                Constants.RedisDB.AdditionalInfoId,
                entity._id.ToString(),
                JsonConvert.SerializeObject(entity));
        }

        public IQueryable<Club> Get(string value)
        {
            // Load from RedisDB id cache
            var objs = _redisClubManager.ClubRepository.GetSet(
                Constants.RedisDB.ClubEntityName,
                Constants.RedisDB.AdditionalInfoName,
                value);

            // Load from RedisDB obj cache
            List<Club> clubs = new List<Club>();
            foreach (var item in objs)
            {
                var club = _redisClubManager.ClubRepository.GetString(
                    Constants.RedisDB.ClubEntityName,
                    Constants.RedisDB.AdditionalInfoId,
                    item);

                // Load then from MongoDB
                if (string.IsNullOrEmpty(club))
                {
                    var entity = _mongoClubManager.ClubRepository.Get(item);
                    if (entity != null)
                    {
                        clubs.Add(entity);
                    }
                    // TODO: here we can have the situation where there are elements in RedisDB but they do not exist in MongoDB
                    // make this so when the redis have and MongoDB doesn't remove it from redis.
                    continue;
                }
                // Load then from RedisDB obj cache
                clubs.Add(JsonConvert.DeserializeObject<Club>(club));
            }

            return clubs.AsQueryable<Club>();
        }

        public void Update(Club entity)
        {
            // Update _id to RedisDB id cache
            _redisClubManager.ClubRepository.StoreSet(
                Constants.RedisDB.ClubEntityName,
                Constants.RedisDB.AdditionalInfoName,
                entity.GetNameWithoutSpaces(),
                entity._id.ToString());
            // Update obj to RedisDB obj cache
            _redisClubManager.ClubRepository.StoreString(
                Constants.RedisDB.ClubEntityName,
                Constants.RedisDB.AdditionalInfoId,
                entity._id.ToString(),
                JsonConvert.SerializeObject(entity));
            // Update MongoDB            
            _mongoClubManager.ClubRepository.Update(item => item._id, entity._id, entity);
        }

        public void Delete(string id)
        {
            // Remove from MongoDB
            var entity = _mongoClubManager.ClubRepository.Delete(item => item._id, id);
            if (entity == null)
            {
                return;
            }
            // Remove from RedisDB id cache
            _redisClubManager.ClubRepository.RemoveSet(
                Constants.RedisDB.ClubEntityName,
                Constants.RedisDB.AdditionalInfoName,
                entity.GetNameWithoutSpaces(),
                entity._id.ToString());
            // Remove from RedisDB obj cache
            _redisClubManager.ClubRepository.RemoveString(
                Constants.RedisDB.ClubEntityName,
                Constants.RedisDB.AdditionalInfoId,
                entity._id.ToString());
        }
    }
}
