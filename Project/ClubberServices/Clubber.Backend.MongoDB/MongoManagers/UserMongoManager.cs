using Clubber.Backend.Models.Model;
using Clubber.Backend.MongoDB.MongoRepository;
using MongoDB.Driver;

namespace Clubber.Backend.MongoDB.MongoManagers
{
    public class UserMongoManager
    {
        //Db
        private IMongoDatabase _database;

        //Repository
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

        //Constructor
        public UserMongoManager(string connectionString, string databaseName)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new System.Exception("Mongo connection string is empty!");
            }

            if (string.IsNullOrEmpty(databaseName))
            {
                throw new System.Exception("Mongo database name is empty!");
            }

            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }
    }
}
