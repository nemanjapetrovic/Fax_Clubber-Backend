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

        public Manager Get(string id)
        {
            return _managerMongoManager.ManagerRepository.Get(new ObjectId(id));
        }

        public void Update(Manager entity)
        {
            _managerMongoManager.ManagerRepository.Update(item => item._id, entity._id, entity);
        }

        public void Delete(string id)
        {
            _managerMongoManager.ManagerRepository.Delete(item => item._id, new ObjectId(id));
        }
    }
}
