﻿using Clubber.Backend.Models.Model;
using Clubber.Backend.MongoDB.DependencyContainerRef;
using Clubber.Backend.MongoDB.MongoRepository;
using MongoDB.Driver;
using System;

namespace Clubber.Backend.MongoDB.MongoManagers
{
    public class EventMongoManager
    {
        private IMongoDatabase _database;

        protected IMongoRepository<Event> _eventRepo = null;
        public IMongoRepository<Event> EventRepository
        {
            get
            {
                if (_eventRepo == null)
                {
                    _eventRepo = new MongoRepository<Event>(_database, "event");
                }
                return _eventRepo;
            }
        }

        public EventMongoManager(string connectionString, string databaseName)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("Mongo connection string is empty!");
            }

            if (string.IsNullOrEmpty(databaseName))
            {
                throw new Exception("Mongo database name is empty!");
            }

            // Get a database
            _database = DependencyContainer.Instance
                .MongoClient(connectionString).GetDatabase(databaseName);
        }
    }
}