using Clubber.Backend.Models.DomainModels;
using Clubber.Backend.MongoDB.DomainModelMongoManagers;
using System.Collections.Generic;
using System.Linq;

namespace Clubber.Backend.Services.Logic.DomainModelServices
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

        public IQueryable<User> Get(string value)
        {
            var user = _mongoUserManager.UserRepository.Get(value);
            IList<User> list = new List<User>();

            if (user != null)
            {
                list.Add(user);
            }

            return list.AsQueryable();
        }

        public void Update(User entity)
        {
            _mongoUserManager.UserRepository.Update(item => item._id, entity._id, entity);
        }

        public void Delete(string id)
        {
            _mongoUserManager.UserRepository.Delete(item => item._id, id);
        }
    }
}
