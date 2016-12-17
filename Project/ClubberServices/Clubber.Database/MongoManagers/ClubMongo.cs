using Clubber.Database.Helpers;
using Clubber.Database.Model;
using Clubber.Database.MongoRepository;
using MongoDB.Driver;
using System.Configuration;

namespace Clubber.Database.MongoManagers
{
    public class ClubMongo
    {
        //Db
        private IMongoDatabase _database;

        //Repo
        protected MongoRepository<Club> _clubRepo = null;
        public MongoRepository<Club> ClubRepository
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
        public ClubMongo()
        {
            var connectionString = ConfigurationManager.AppSettings[Constants.MongoDB.MongoDBConectionString];
            var client = new MongoClient(connectionString);
            var databaseName = ConfigurationManager.AppSettings[Constants.MongoDB.MongoDBDatabaseNameTest];
            _database = client.GetDatabase(databaseName);
        }
    }
}
