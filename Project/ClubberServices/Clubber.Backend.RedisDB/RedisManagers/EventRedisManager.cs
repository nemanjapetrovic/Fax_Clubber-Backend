using Clubber.Backend.RedisDB.RedisRepository;

namespace Clubber.Backend.RedisDB.RedisManagers
{
    public class EventRedisManager
    {
        protected IRedisRepository _eventRepo = null;
        public IRedisRepository EventRepository
        {
            get
            {
                if (_eventRepo == null)
                {
                    _eventRepo = new RedisRepository.RedisRepository();
                }
                return _eventRepo;
            }
        }
    }
}
