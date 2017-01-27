using System.Linq;
using Clubber.Backend.Models.Model;
using Clubber.Backend.MongoDB.MongoManagers;
using MongoDB.Bson;
using Clubber.Backend.Services.Logic.Helpers;
using Clubber.Backend.RedisDB.RedisManagers;
using System.Collections.Generic;

namespace Clubber.Backend.MongoDB.MongoServices
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
            //Add to MongoDB
            _mongoClubManager.ClubRepository.Add(entity);
            //Add _id to RedisDB Sets
            _redisClubManager.ClubRepository.Store(
                Constants.RedisDB.ClubEntityName,
                Constants.RedisDB.AdditionalInfoName,
                entity.GetNameWithoutSpaces(),
                entity._id.ToString());
        }

        public IQueryable<Club> Get()
        {
            //Get all items from MongoDB
            return _mongoClubManager.ClubRepository.Get();
        }

        public IQueryable<Club> Get(string value)
        {
            //Load from RedisDB
            var objs = _redisClubManager.ClubRepository.Get(
                Constants.RedisDB.ClubEntityName,
                Constants.RedisDB.AdditionalInfoName,
                value);

            //Load from MongoDB
            List<Club> clubs = new List<Club>();
            foreach (var item in objs)
            {
                clubs.Add(_mongoClubManager.ClubRepository.Get(new ObjectId(item)));
            }

            return clubs.AsQueryable<Club>();
        }

        public void Update(Club entity)
        {
            //Only update in MongoDB
            _mongoClubManager.ClubRepository.Update(item => item._id, entity._id, entity);
        }

        public void Delete(string id)
        {
            //Remove from MongoDB
            var entity = _mongoClubManager.ClubRepository.Delete(item => item._id, new ObjectId(id));
            //Remove from RedisDB
            _redisClubManager.ClubRepository.Remove(
                Constants.RedisDB.ClubEntityName,
                Constants.RedisDB.AdditionalInfoName,
                entity.GetNameWithoutSpaces(),
                entity._id.ToString());
        }
    }
}
