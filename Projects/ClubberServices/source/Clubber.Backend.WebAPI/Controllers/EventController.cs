using Clubber.Backend.Models.DomainModels;
using Clubber.Backend.Models.LogModels;
using Clubber.Backend.Services.Logic.DomainModelServices;
using Clubber.Backend.Services.Logic.LogServices;
using Clubber.Backend.WebAPI.Helpers;
using Clubber.Common.Exceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace Eventber.WebAPI.Controllers
{
    public class EventController : ApiController
    {
        private readonly IService<Event> _iEventService;
        private readonly ILogService _logService;

        public EventController()
        {
            // Connection strings
            string mongoConStr = ConfigurationManager.ConnectionStrings[Constants.MongoDB.MongoDBConectionString].ConnectionString;
            string redisConStr = ConfigurationManager.ConnectionStrings[Constants.RedisDB.RedisDBConectionString].ConnectionString;

            // MongoDB name
            string mongoDbName = ConfigurationManager.AppSettings[Constants.MongoDB.MongoDBDatabaseName];
            string logMongoDbName = ConfigurationManager.AppSettings[Constants.MongoDB.LogMongoDBDatabaseName];

            _iEventService = new EventService(mongoConStr, mongoDbName, redisConStr);
            _logService = new LogService(mongoConStr, logMongoDbName);
        }

        // GET: api/Event/5
        public IEnumerable<Event> Get(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    return new List<Event>();
                }

                var obj = _iEventService.Get(Regex.Replace(name, @"\s+", "").ToLower());
                return obj;
            }
            catch (InternalServerErrorException ex)
            {
                // Log
                _logService.Add(
                    _logService.CreateLogModel(DateTime.Now,
                    "GET",
                    $"Internal server error, {ex.Message}",
                    LogType.Exception));

                return new List<Event>();
            }
            catch (Exception ex)
            {
                // Log
                _logService.Add(
                    _logService.CreateLogModel(DateTime.Now,
                    "GET",
                    $"Exception error, {ex.Message}",
                    LogType.Exception));

                return new List<Event>();
            }
        }

        // POST: api/Event
        public IHttpActionResult Post([FromBody]Event value)
        {
            try
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
            catch (InternalServerErrorException ex)
            {
                // Log
                _logService.Add(
                    _logService.CreateLogModel(DateTime.Now,
                    "POST",
                    $"Internal server error, {ex.Message}",
                    LogType.Exception));

                return InternalServerError();
            }
            catch (Exception ex)
            {
                // Log
                _logService.Add(
                    _logService.CreateLogModel(DateTime.Now,
                    "POST",
                    $"Exception error, {ex.Message}",
                    LogType.Exception));
                return InternalServerError();
            }
        }

        // PUT: api/Event/5
        public IHttpActionResult Put(string id, [FromBody]Event value)
        {
            try
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
            catch (InternalServerErrorException ex)
            {
                // Log
                _logService.Add(
                    _logService.CreateLogModel(DateTime.Now,
                    "PUT",
                    $"Internal server error, {ex.Message}",
                    LogType.Exception));

                return InternalServerError();
            }
            catch (Exception ex)
            {
                // Log
                _logService.Add(
                    _logService.CreateLogModel(DateTime.Now,
                    "PUT",
                    $"Exception error, {ex.Message}",
                    LogType.Exception));

                return InternalServerError();
            }
        }

        // DELETE: api/Event/5
        public IHttpActionResult Delete(string id)
        {
            try
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
            catch (InternalServerErrorException ex)
            {
                // Log
                _logService.Add(
                    _logService.CreateLogModel(DateTime.Now,
                    "DELETE",
                    $"Internal server error, {ex.Message}",
                    LogType.Exception));

                return InternalServerError();
            }
            catch (Exception ex)
            {
                // Log
                _logService.Add(
                    _logService.CreateLogModel(DateTime.Now,
                    "DELETE",
                    $"Exception error, {ex.Message}",
                    LogType.Exception));

                return InternalServerError();
            }
        }
    }
}
