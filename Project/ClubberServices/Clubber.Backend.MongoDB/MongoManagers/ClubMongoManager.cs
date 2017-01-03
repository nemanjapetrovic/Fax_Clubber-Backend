using Clubber.Backend.Models.Model;
using Clubber.Backend.MongoDB.Helpers;
using Clubber.Backend.MongoDB.MongoRepository;
using MongoDB.Driver;

namespace Clubber.Backend.MongoDB.MongoManagers
{
    public class ClubMongoManager
    {
        //Db
        private IMongoDatabase _database;

        //Repo
        protected IMongoRepository<Club> _clubRepo = null;
        public IMongoRepository<Club> ClubRepository
        {
            get
            {
                if (_clubRepo == null)
                {
                    _clubRepo = new MongoRepository<Club>(_database, "club");
                }
                return _clubRepo;
            }
        }

        //Constructor
        public ClubMongoManager()
        {
            var connectionString = Constants.MongoDB.MongoDBConectionString;
            var client = new MongoClient(connectionString);
            var databaseName = Constants.MongoDB.MongoDBDatabaseNameTest;
            _database = client.GetDatabase(databaseName);
        }
    }
}
