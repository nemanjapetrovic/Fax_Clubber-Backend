using Clubber.Backend.Models.Model;
using Clubber.Backend.MongoDB.DependencyInjectionContainer;
using Clubber.Backend.MongoDB.MongoRepository;
using MongoDB.Driver;
using System;

namespace Clubber.Backend.MongoDB.MongoManagers
{
    public class ClubMongoManager
    {
        private IMongoDatabase _database;

        protected IMongoRepository<Club> _clubRepo = null;
        public IMongoRepository<Club> ClubRepository
        {
            get
            {
                if (_clubRepo == null)
                {
                    _clubRepo = new MongoRepository<Club>(_database, "club");
                }
                return _clubRepo;
            }
        }

        public ClubMongoManager(string connectionString, string databaseName)
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