using Clubber.Backend.RedisDB.RedisRepository;

namespace Clubber.Backend.RedisDB.RedisManagers
{
    public class EventRedisManager
    {
        private string _connectionString;

        protected IRedisRepository _eventRepo = null;
        public IRedisRepository EventRepository
        {
            get
            {
                if (_eventRepo == null)
                {
                    _eventRepo = new RedisRepository.RedisRepository(_connectionString);
                }
                return _eventRepo;
            }
        }

        public EventRedisManager(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new System.Exception("Redis connection string is empty!");
            }

            _connectionString = connectionString;
        }
    }
}
