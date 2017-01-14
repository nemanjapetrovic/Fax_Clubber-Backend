using Clubber.Backend.Models.Model;
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

            // Create a client and get a database.
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }
    }
}
