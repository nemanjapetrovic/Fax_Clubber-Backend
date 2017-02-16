﻿using Clubber.Backend.MongoDB.DomainModelMongoManagers;
using System.Collections.Generic;
using System.Linq;
using LogModel = Clubber.Backend.Models.LogModels.Log;

namespace Clubber.Backend.Services.Logic.LogServices
{
    public class LogService
    {
        private readonly LogMongoManager _mongoLogManager;

        public LogService(string mongoConStr, string mongoDbName)
        {
            _mongoLogManager = new LogMongoManager(mongoConStr, mongoDbName);
        }

        public void Add(LogModel entity)
        {
            _mongoLogManager.LogRepository.Add(entity);
        }

        public IQueryable<LogModel> Get(int skip, int limit)
        {
            return _mongoLogManager.LogRepository.Get(skip, limit);
        }

        public IQueryable<LogModel> Get(string id)
        {
            var log = _mongoLogManager.LogRepository.Get(id);
            IList<LogModel> list = new List<LogModel>();

            if (log != null)
            {
                list.Add(log);
            }

            return list.AsQueryable();
        }

        public void Delete(string id)
        {
            _mongoLogManager.LogRepository.Delete(item => item._id, id);
        }
    }
}
