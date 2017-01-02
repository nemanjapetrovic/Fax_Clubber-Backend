﻿using System.Linq;
using Clubber.Backend.Models.Model;
using Clubber.Backend.MongoDB.MongoManagers;
using MongoDB.Bson;

namespace Clubber.Backend.MongoDB.MongoServices
{
    public class ClubService : IMongoService<Club>
    {
        private readonly ClubMongoManager _clubMongoManager;

        public ClubService()
        {
            _clubMongoManager = new ClubMongoManager();
        }

        public void Add(Club entity)
        {
            _clubMongoManager.ClubRepository.Add(entity);
        }

        public IQueryable<Club> Get()
        {
            return _clubMongoManager.ClubRepository.Get();
        }

        public Club Get(string id)
        {
            return _clubMongoManager.ClubRepository.Get(new ObjectId(id));
        }

        public void Update(Club entity)
        {
            _clubMongoManager.ClubRepository.Update(item => item._id, entity._id, entity);
        }

        public void Delete(string id)
        {
            _clubMongoManager.ClubRepository.Delete(item => item._id, new ObjectId(id));
        }
    }
}
