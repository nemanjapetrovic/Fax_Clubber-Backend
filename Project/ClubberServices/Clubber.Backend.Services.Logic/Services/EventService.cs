using Clubber.Backend.Models.Model;
using Clubber.Backend.MongoDB.MongoManagers;
using System.Linq;
using MongoDB.Bson;

namespace Clubber.Backend.MongoDB.MongoServices
{
    public class EventService : IMongoService<Event>
    {
        private readonly EventMongoManager _eventMongoManager;

        public EventService()
        {
            _eventMongoManager = new EventMongoManager();
        }

        public void Add(Event entity)
        {
            _eventMongoManager.EventRepository.Add(entity);
        }

        public IQueryable<Event> Get()
        {
            return _eventMongoManager.EventRepository.Get();
        }

        public Event Get(string id)
        {
            return _eventMongoManager.EventRepository.Get(id);
        }

        public void Update(Event entity)
        {
            _eventMongoManager.EventRepository.Update(item => item.ID.ToString(), entity.ID.ToString(), entity);
        }

        public void Delete(string id)
        {
            _eventMongoManager.EventRepository.Delete(item => item.ID.ToString(), id);
        }
    }
}
