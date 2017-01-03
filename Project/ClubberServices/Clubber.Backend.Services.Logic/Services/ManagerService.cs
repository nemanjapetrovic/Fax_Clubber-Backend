using Clubber.Backend.Models.Model;
using Clubber.Backend.MongoDB.MongoManagers;
using System.Linq;
using MongoDB.Bson;

namespace Clubber.Backend.MongoDB.MongoServices
{
    public class ManagerService : IService<Manager>
    {
        private readonly ManagerMongoManager _mongoManagerManager;

        public ManagerService()
        {
            _mongoManagerManager = new ManagerMongoManager();
        }

        public void Add(Manager entity)
        {
            _mongoManagerManager.ManagerRepository.Add(entity);
        }

        public IQueryable<Manager> Get()
        {
            return _mongoManagerManager.ManagerRepository.Get();
        }

        public Manager Get(string id)
        {
            return _mongoManagerManager.ManagerRepository.Get(new ObjectId(id));
        }

        public void Update(Manager entity)
        {
            _mongoManagerManager.ManagerRepository.Update(item => item._id, entity._id, entity);
        }

        public void Delete(string id)
        {
            _mongoManagerManager.ManagerRepository.Delete(item => item._id, new ObjectId(id));
        }
    }
}
