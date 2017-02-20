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
using System.Web.Http;

namespace Managerber.WebAPI.Controllers
{
    public class ManagerController : ApiController
    {
        private readonly IService<Manager> _iManagerService;
        private readonly ILogService _logService;

        public ManagerController()
        {
            // Connection strings
            string mongoConStr = ConfigurationManager.ConnectionStrings[Constants.MongoDB.MongoDBConectionString].ConnectionString;
            string redisConStr = ConfigurationManager.ConnectionStrings[Constants.RedisDB.RedisDBConectionString].ConnectionString;

            // MongoDB name
            string mongoDbName = ConfigurationManager.AppSettings[Constants.MongoDB.MongoDBDatabaseName];
            string logMongoDbName = ConfigurationManager.AppSettings[Constants.MongoDB.LogMongoDBDatabaseName];

            _iManagerService = new ManagerService(mongoConStr, mongoDbName, redisConStr);
            _logService = new LogService(mongoConStr, logMongoDbName);
        }

        // GET: api/Manager/5
        public IEnumerable<Manager> Get(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return new List<Manager>();
                }

                var obj = _iManagerService.Get(id);
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

                return new List<Manager>();
            }
            catch (Exception ex)
            {
                // Log
                _logService.Add(
                    _logService.CreateLogModel(DateTime.Now,
                    "GET",
                    $"Exception error, {ex.Message}",
                    LogType.Exception));

                return new List<Manager>();
            }
        }

        // POST: api/Manager
        public IHttpActionResult Post([FromBody]Manager value)
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
                _iManagerService.Add(value);

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

        // PUT: api/Manager/5
        public IHttpActionResult Put(string id, [FromBody]Manager value)
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
                _iManagerService.Update(value);

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

        // DELETE: api/Manager/5
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
                _iManagerService.Delete(id);

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
