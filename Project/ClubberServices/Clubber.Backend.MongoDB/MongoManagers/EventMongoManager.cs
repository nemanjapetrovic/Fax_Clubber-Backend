using Clubber.Backend.Models.Model;
using Clubber.Backend.MongoDB.MongoRepository;
using MongoDB.Driver;

namespace Clubber.Backend.MongoDB.MongoManagers
{
    public class EventMongoManager
    {
        //Db
        private IMongoDatabase _database;

        //Repository
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

        //Constructor
        public EventMongoManager(string connectionString, string databaseName)
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