using Clubber.Backend.Models.DomainModels;
using Clubber.Backend.MongoDB.DomainModelMongoManagers;
using System.Linq;
using System.Collections.Generic;

namespace Clubber.Backend.Services.Logic.DomainModelServices
{
    public class ManagerService : IService<Manager>
    {
        private readonly ManagerMongoManager _mongoManagerManager;

        public ManagerService(string mongoConStr,
                           string mongoDbName,
                           string redisConStr)
        {
            _mongoManagerManager = new ManagerMongoManager(mongoConStr, mongoDbName);
        }

        public void Add(Manager entity)
        {
            _mongoManagerManager.ManagerRepository.Add(entity);
        }

        public IQueryable<Manager> Get(string id)
        {
            var manager = _mongoManagerManager.ManagerRepository.Get(id);
            IList<Manager> list = new List<Manager>();

            if (manager != null)
            {
                list.Add(manager);
            }

            return list.AsQueryable();
        }

        public void Update(Manager entity)
        {
            _mongoManagerManager.ManagerRepository.Update(item => item._id, entity._id, entity);
        }

        public void Delete(string id)
        {
            _mongoManagerManager.ManagerRepository.Delete(item => item._id, id);
        }
    }
}
