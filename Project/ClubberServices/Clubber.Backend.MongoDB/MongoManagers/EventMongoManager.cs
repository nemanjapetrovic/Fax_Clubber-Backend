using Clubber.Backend.MongoDB.Helpers;
using Clubber.Backend.Models.Model;
using Clubber.Backend.MongoDB.MongoRepository;
using MongoDB.Driver;
using System.Configuration;

namespace Clubber.Backend.MongoDB.MongoManagers
{
    public class EventMongoManager
    {
        //Db
        private IMongoDatabase _database;

        //Repo
        protected MongoRepository<Event> _eventRepo;
        public MongoRepository<Event> EventRepository
        {
            get
            {
                if (_eventRepo == null)
                {
                    _eventRepo = new MongoRepository<Event>(_database, "event");
                }
                return _eventRepo;
            }
        }

        //Const
        public EventMongoManager()
        {
            var connectionString = ConfigurationManager.AppSettings[Constants.MongoDB.MongoDBConectionString];
            var client = new MongoClient(connectionString);
            var databaseName = ConfigurationManager.AppSettings[Constants.MongoDB.MongoDBDatabaseNameTest];
            _database = client.GetDatabase(databaseName);
        }
    }

}