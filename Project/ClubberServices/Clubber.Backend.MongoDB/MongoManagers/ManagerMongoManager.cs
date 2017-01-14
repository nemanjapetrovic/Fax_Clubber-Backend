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

            // Create a client
            var client = new MongoClient(connectionString);

            // Check the connection
            if (client.Cluster.Description.State.ToString().ToLower().Equals("disconnected"))
            {
                throw new Exception("MongoDB is not connected!");
            }

            // Get a database
            _database = client.GetDatabase(databaseName);
        }
    }
}
