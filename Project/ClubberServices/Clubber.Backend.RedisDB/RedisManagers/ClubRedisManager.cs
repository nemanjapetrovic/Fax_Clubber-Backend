using Clubber.Backend.RedisDB.RedisRepository;

namespace Clubber.Backend.RedisDB.RedisManagers
{
    public class ClubRedisManager
    {
        protected IRedisRepository _clubRepo = null;
        public IRedisRepository ClubRepository
        {
            get
            {
                if (_clubRepo == null)
                {
                    _clubRepo = new RedisRepository.RedisRepository();
                }
                return _clubRepo;
            }
        }
    }
}
