using Clubber.Backend.Models.Model;
using Clubber.Backend.MongoDB.MongoServices;
using System.Collections.Generic;
using System.Web.Http;

namespace Eventber.WebAPI.Controllers
{
    public class EventController : ApiController
    {
        private readonly IMongoService<Event> _iEventService;

        public EventController()
        {
            _iEventService = new EventService();
        }

        // GET: api/Event
        public IEnumerable<Event> Get()
        {
            var objs = _iEventService.Get();
            return objs;
        }

        // GET: api/Event/5
        public Event Get(ObjectId id)
        {
            var obj = _iEventService.Get(id);
            return obj;
        }

        // POST: api/Event
        public void Post([FromBody]Event value)
        {
            _iEventService.Add(value);

        }

        // PUT: api/Event/5
        public void Put(ObjectId id, [FromBody]Event value)
        {
            value.ID = id;
            _iEventService.Update(value);
        }

        // DELETE: api/Event/5
        public void Delete(ObjectId id)
        {
            _iEventService.Delete(id);
        }
    }
}
