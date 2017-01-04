using Clubber.Backend.Models.Model;
using Clubber.Backend.MongoDB.MongoRepository;
using MongoDB.Driver;

namespace Clubber.Backend.MongoDB.MongoManagers
{
    public class ManagerMongoManager
    {
        //Db
        private IMongoDatabase _database;

        //Repository
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

        //Constructor
        public ManagerMongoManager(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }
    }
}
