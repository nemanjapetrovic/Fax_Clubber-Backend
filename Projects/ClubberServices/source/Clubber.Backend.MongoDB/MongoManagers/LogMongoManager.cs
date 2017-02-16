using Clubber.Backend.Models.LogModels;
using Clubber.Backend.MongoDB.DependencyInjectionContainer;
using Clubber.Backend.MongoDB.MongoRepository;
using Clubber.Common.Exceptions.Exceptions;
using MongoDB.Driver;

namespace Clubber.Backend.MongoDB.MongoManagers
{
    public class LogMongoManager
    {
        private IMongoDatabase _database;

        protected IMongoRepository<Log> _logRepo = null;
        public IMongoRepository<Log> LogRepository
        {
            get
            {
                if (_logRepo == null)
                {
                    _logRepo = new MongoRepository<Log>(_database, "log");
                }
                return _logRepo;
            }
        }

        public LogMongoManager(string connectionString, string databaseName)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InternalServerErrorException("Mongo connection string is empty!");
            }

            if (string.IsNullOrEmpty(databaseName))
            {
                throw new InternalServerErrorException("Mongo database name is empty!");
            }

            // Get a database
            _database = DependencyContainer.Instance
                .MongoClient(connectionString).GetDatabase(databaseName);
        }
    }
}
