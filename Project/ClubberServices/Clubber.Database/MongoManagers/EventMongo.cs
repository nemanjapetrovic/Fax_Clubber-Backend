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
    public class EventMongo
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
        public EventMongo()
        {
            var connectionString = ConfigurationManager.AppSettings[Constants.MongoDB.MongoDBConectionString];
            var client = new MongoClient(connectionString);
            var databaseName = ConfigurationManager.AppSettings[Constants.MongoDB.MongoDBDatabaseNameTest];
            _database = client.GetDatabase(databaseName);
        }
    }

}