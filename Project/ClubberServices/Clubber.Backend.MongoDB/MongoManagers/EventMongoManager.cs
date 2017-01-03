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
        protected IMongoRepository<Event> _eventRepo = null;
        public IMongoRepository<Event> EventRepository
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
            var connectionString = Constants.MongoDB.MongoDBConectionString;
            var client = new MongoClient(connectionString);
            var databaseName = Constants.MongoDB.MongoDBDatabaseNameTest;
            _database = client.GetDatabase(databaseName);
        }
    }

}