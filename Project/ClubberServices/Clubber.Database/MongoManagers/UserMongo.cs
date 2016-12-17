using Clubber.Database.Helpers;
using Clubber.Database.Model;
using Clubber.Database.MongoRepository;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clubber.Database.MongoManagers
{
    public class UserMongo
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
        public UserMongo()
        {
            var connectionString = ConfigurationManager.AppSettings[Constants.MongoDB.MongoDBConectionString];
            var client = new MongoClient(connectionString);
            var databaseName = ConfigurationManager.AppSettings[Constants.MongoDB.MongoDBDatabaseNameTest];
            _database = client.GetDatabase(databaseName);
        }
    }
}
