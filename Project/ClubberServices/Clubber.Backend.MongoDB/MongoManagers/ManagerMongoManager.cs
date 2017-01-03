using Clubber.Backend.MongoDB.Helpers;
using Clubber.Backend.Models.Model;
using Clubber.Backend.MongoDB.MongoRepository;
using MongoDB.Driver;
using System.Configuration;

namespace Clubber.Backend.MongoDB.MongoManagers
{
    public class ManagerMongoManager
    {
        //Db
        private IMongoDatabase _database;

        //Repo
        protected IMongoRepository<Manager> _managerRepo;
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

        //Const
        public ManagerMongoManager()
        {
            var connectionString = Constants.MongoDB.MongoDBConectionString;
            var client = new MongoClient(connectionString);
            var databaseName = Constants.MongoDB.MongoDBDatabaseNameTest;
            _database = client.GetDatabase(databaseName);
        }
    }
}
