using Clubber.Backend.Models.Model;
using Clubber.Backend.MongoDB.MongoRepository;
using MongoDB.Driver;
using System;

namespace Clubber.Backend.MongoDB.MongoManagers
{
    public class UserMongoManager
    {
        private IMongoDatabase _database;

        protected IMongoRepository<User> _userRepo = null;
        public IMongoRepository<User> UserRepository
        {
            get
            {
                if (_userRepo == null)
                {
                    _userRepo = new MongoRepository<User>(_database, "user");
                }
                return _userRepo;
            }
        }

        public UserMongoManager(string connectionString, string databaseName)
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

            // Get a database
            _database = client.GetDatabase(databaseName);
        }
    }
}
