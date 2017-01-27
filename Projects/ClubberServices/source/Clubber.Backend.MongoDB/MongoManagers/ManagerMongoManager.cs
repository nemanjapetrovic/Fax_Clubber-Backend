using Clubber.Backend.Models.Model;
using Clubber.Backend.MongoDB.DependencyInjectionContainer;
using Clubber.Backend.MongoDB.MongoRepository;
using MongoDB.Driver;
using System;

namespace Clubber.Backend.MongoDB.MongoManagers
{
    public class ManagerMongoManager
    {
        private IMongoDatabase _database;

        protected IMongoRepository<Manager> _managerRepo = null;
        public IMongoRepository<Manager> ManagerRepository
        {
            get
            {
                if (_managerRepo == null)
                {
                    _managerRepo = new MongoRepository<Manager>(_database, "manager");
                }
                return _managerRepo;
            }
        }

        public ManagerMongoManager(string connectionString, string databaseName)
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
