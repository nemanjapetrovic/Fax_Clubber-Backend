using Clubber.Backend.Models.DomainModels;
using Clubber.Backend.MongoDB.DependencyInjectionContainer;
using Clubber.Backend.MongoDB.MongoRepository;
using Clubber.Common.Exceptions.Exceptions;
using MongoDB.Driver;

namespace Clubber.Backend.MongoDB.DomainModelMongoManagers
{
    public class UserMongoManager
    {
        private IMongoDatabase _database;

        protected IMongoRepository<User> _userRepo = null;
        public IMongoRepository<User> UserRepository
        {
            get
            {
                if (_userRepo == null)
                {
                    _userRepo = new MongoRepository<User>(_database, "user");
                }
                return _userRepo;
            }
        }

        public UserMongoManager(string connectionString, string databaseName)
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
