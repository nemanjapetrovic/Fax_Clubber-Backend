using Clubber.Backend.Models.Model;
using Clubber.Backend.MongoDB.MongoManagers;
using System.Linq;
using MongoDB.Bson;

namespace Clubber.Backend.MongoDB.MongoServices
{
    public class UserService : IMongoService<User>
    {
        private readonly UserMongoManager _userMongoManager;

        public UserService()
        {
            _userMongoManager = new UserMongoManager();
        }

        public void Add(User entity)
        {
            _userMongoManager.UserRepository.Add(entity);
        }

        public IQueryable<User> Get()
        {
            return _userMongoManager.UserRepository.Get();
        }

        public User Get(ObjectId id)
        {
            return _userMongoManager.UserRepository.Get(id);
        }

        public void Update(User entity)
        {
            _userMongoManager.UserRepository.Update(item => item.ID, entity.ID, entity);
        }

        public void Delete(ObjectId id)
        {
            _userMongoManager.UserRepository.Delete(item => item.ID, id);
        }
    }
}
