using Clubber.Database.Helpers;
using Clubber.Database.Model;
using Clubber.Database.MongoRepository;
using MongoDB.Driver;
using System.Configuration;

namespace Clubber.Database.MongoManagers
{
    public class ManagerMongo
    {
        //Db
        private IMongoDatabase _database;

        //Repo
        protected MongoRepository<Manager> _managerRepo;
        public MongoRepository<Manager> ManagerRepository
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

        //Const
        public ManagerMongo()
        {
            var connectionString = ConfigurationManager.AppSettings[Constants.MongoDB.MongoDBConectionString];
            var client = new MongoClient(connectionString);
            var databaseName = ConfigurationManager.AppSettings[Constants.MongoDB.MongoDBDatabaseNameTest];
            _database = client.GetDatabase(databaseName);
        }
    }
}
