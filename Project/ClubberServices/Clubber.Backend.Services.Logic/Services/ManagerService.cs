using Clubber.Backend.Models.Model;
using Clubber.Backend.MongoDB.MongoManagers;
using System.Linq;
using MongoDB.Bson;

namespace Clubber.Backend.MongoDB.MongoServices
{
    public class ManagerService : IMongoService<Manager>
    {
        private readonly ManagerMongoManager _managerMongoManager;

        public ManagerService()
        {
            _managerMongoManager = new ManagerMongoManager();
        }

        public void Add(Manager entity)
        {
            _managerMongoManager.ManagerRepository.Add(entity);
        }

        public IQueryable<Manager> Get()
        {
            return _managerMongoManager.ManagerRepository.Get();
        }

        public Manager Get(ObjectId id)
        {
            return _managerMongoManager.ManagerRepository.Get(id);
        }

        public void Update(Manager entity)
        {
            _managerMongoManager.ManagerRepository.Update(item => item.ID, entity.ID, entity);
        }

        public void Delete(ObjectId id)
        {
            _managerMongoManager.ManagerRepository.Delete(item => item.ID, id);
        }
    }
}
