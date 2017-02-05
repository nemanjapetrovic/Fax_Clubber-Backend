using Clubber.Backend.Models.Model;
using Clubber.Backend.Services.Logic.Services;
using Clubber.Backend.WebAPI.Helpers;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;

namespace Eventber.WebAPI.Controllers
{
    public class EventController : ApiController
    {
        private readonly IService<Event> _iEventService;

        public EventController()
        {
            //connection strings
            string mongoConStr = ConfigurationManager.ConnectionStrings[Constants.MongoDB.MongoDBConectionString].ConnectionString;
            string redisConStr = ConfigurationManager.ConnectionStrings[Constants.RedisDB.RedisDBConectionString].ConnectionString;
            //mongodb name
            string mongoDbName = ConfigurationManager.AppSettings[Constants.MongoDB.MongoDBDatabaseName];

            _iEventService = new EventService(mongoConStr, mongoDbName, redisConStr);
        }

        // GET: api/Event/5
        public IEnumerable<Event> Get(string id)
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
        public void Put(string id, [FromBody]Event value)
        {
            value._id = new ObjectId(id);
            _iEventService.Update(value);
        }

        // DELETE: api/Event/5
        public void Delete(string id)
        {
            _iEventService.Delete(id);
        }
    }
}
