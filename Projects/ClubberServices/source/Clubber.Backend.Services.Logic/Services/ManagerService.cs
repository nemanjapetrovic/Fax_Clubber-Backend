﻿using Clubber.Backend.Models.Model;
using Clubber.Backend.MongoDB.MongoManagers;
using System.Linq;
using MongoDB.Bson;
using System.Collections.Generic;

namespace Clubber.Backend.Services.Logic.Services
{
    public class ManagerService : IService<Manager>
    {
        private readonly ManagerMongoManager _mongoManagerManager;

        public ManagerService(string mongoConStr,
                           string mongoDbName,
                           string redisConStr)
        {
            _mongoManagerManager = new ManagerMongoManager(mongoConStr, mongoDbName);
        }

        public void Add(Manager entity)
        {
            _mongoManagerManager.ManagerRepository.Add(entity);
        }

        public IQueryable<Manager> Get(string id)
        {
            var manager = _mongoManagerManager.ManagerRepository.Get(id);
            IList<Manager> list = new List<Manager>();
            list.Add(manager);

            return list.AsQueryable();
        }

        public void Update(Manager entity)
        {
            _mongoManagerManager.ManagerRepository.Update(item => item._id, entity._id, entity);
        }

        public void Delete(string id)
        {
            _mongoManagerManager.ManagerRepository.Delete(item => item._id, id);
        }
    }
}
