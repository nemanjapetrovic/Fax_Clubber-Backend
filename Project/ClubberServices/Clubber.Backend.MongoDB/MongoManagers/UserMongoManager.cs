using Clubber.Backend.MongoDB.Helpers;
using Clubber.Backend.Models.Model;
using Clubber.Backend.MongoDB.MongoRepository;
using MongoDB.Driver;
using System.Configuration;

namespace Clubber.Backend.MongoDB.MongoManagers
{
    public class UserMongoManager
    {
        //Db
        private IMongoDatabase _database;

        //Repo
        protected MongoRepository<User> _userRepo = null;
        public MongoRepository<User> UserRepository
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

        //Constructor
        public UserMongoManager()
        {
            var connectionString = Constants.MongoDB.MongoDBConectionString;
            var client = new MongoClient(connectionString);
            var databaseName = Constants.MongoDB.MongoDBDatabaseNameTest;
            _database = client.GetDatabase(databaseName);
        }
    }
}
