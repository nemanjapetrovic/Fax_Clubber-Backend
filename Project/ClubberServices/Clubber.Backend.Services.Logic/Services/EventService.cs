using Clubber.Backend.Models.Model;
using Clubber.Backend.MongoDB.MongoManagers;
using System.Linq;
using MongoDB.Bson;

namespace Clubber.Backend.MongoDB.MongoServices
{
    public class EventService : IService<Event>
    {
        private readonly EventMongoManager _mongoEventManager;

        public EventService()
        {
            _mongoEventManager = new EventMongoManager();
        }

        public void Add(Event entity)
        {
            _mongoEventManager.EventRepository.Add(entity);
        }

        public IQueryable<Event> Get()
        {
            return _mongoEventManager.EventRepository.Get();
        }

        public Event Get(string id)
        {
            return _mongoEventManager.EventRepository.Get(new ObjectId(id));
        }

        public void Update(Event entity)
        {
            _mongoEventManager.EventRepository.Update(item => item._id, entity._id, entity);
        }

        public void Delete(string id)
        {
            _mongoEventManager.EventRepository.Delete(item => item._id, new ObjectId(id));
        }
    }
}