using Clubber.Backend.Models.Model;
using Clubber.Backend.MongoDB.MongoManagers;
using System.Linq;
using MongoDB.Bson;
using System.Collections.Generic;

namespace Clubber.Backend.Services.Logic.Services
{
    public class UserService : IService<User>
    {
        private readonly UserMongoManager _mongoUserManager;

        public UserService(string mongoConStr,
                           string mongoDbName,
                           string redisConStr)
        {
            _mongoUserManager = new UserMongoManager(mongoConStr, mongoDbName);
        }

        public void Add(User entity)
        {
            _mongoUserManager.UserRepository.Add(entity);
        }

        public IQueryable<User> Get(string id)
        {
            var user = _mongoUserManager.UserRepository.Get(new ObjectId(id));
            IList<User> list = new List<User>();
            list.Add(user);

            return list.AsQueryable();
        }

        public void Update(User entity)
        {
            _mongoUserManager.UserRepository.Update(item => item._id, entity._id, entity);
        }

        public void Delete(string id)
        {
            _mongoUserManager.UserRepository.Delete(item => item._id, new ObjectId(id));
        }
    }
}
