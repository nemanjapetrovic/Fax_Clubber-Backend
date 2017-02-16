using Clubber.Backend.Models.DomainModels;
using Clubber.Backend.MongoDB.DependencyInjectionContainer;
using Clubber.Backend.MongoDB.MongoRepository;
using Clubber.Common.Exceptions.Exceptions;
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
                throw new InternalServerErrorException("Mongo connection string is empty!");
            }

            if (string.IsNullOrEmpty(databaseName))
            {
                throw new InternalServerErrorException("Mongo database name is empty!");
            }

            // Get a database
            _database = DependencyContainer.Instance
                .MongoClient(connectionString).GetDatabase(databaseName);
        }
    }
}