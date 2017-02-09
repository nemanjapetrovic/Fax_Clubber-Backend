using Clubber.Backend.Models.Model;
using Clubber.Backend.Services.Logic.Services;
using Clubber.Backend.WebAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;

namespace Eventber.WebAPI.Controllers
{
    public class EventController : ApiController
    {
        private readonly IService<Event> _iEventService;

        public EventController()
        {
            // Connection strings
            string mongoConStr = ConfigurationManager.ConnectionStrings[Constants.MongoDB.MongoDBConectionString].ConnectionString;
            string redisConStr = ConfigurationManager.ConnectionStrings[Constants.RedisDB.RedisDBConectionString].ConnectionString;
            // MongoDB name
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
        public IHttpActionResult Post([FromBody]Event value)
        {
            // Validation
            if (!ModelState.IsValid)
            {
                string messages = string.Join(Environment.NewLine, ModelState.Values
                                           .SelectMany(x => x.Errors)
                                           .Select(x => x.ErrorMessage));
                return BadRequest(messages);
            }

            // Call
            _iEventService.Add(value);

            // Ret
            return Ok();
        }

        // PUT: api/Event/5
        public IHttpActionResult Put(string id, [FromBody]Event value)
        {
            // Validation
            if (!ModelState.IsValid)
            {
                string messages = string.Join(Environment.NewLine, ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                return BadRequest(messages);
            }

            // Call
            value._id = id;
            _iEventService.Update(value);

            // Ret
            return Ok();
        }

        // DELETE: api/Event/5
        public IHttpActionResult Delete(string id)
        {
            // Validation
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Id is empty");
            }

            // Call
            _iEventService.Delete(id);

            // Ret
            return Ok();
        }
    }
}
