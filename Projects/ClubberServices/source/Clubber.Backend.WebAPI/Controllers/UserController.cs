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

namespace Userber.WebAPI.Controllers
{
    public class UserController : ApiController
    {
        private readonly IService<User> _iUserService;
        private readonly ILogService _logService;

        public UserController()
        {
            // Connection strings
            string mongoConStr = ConfigurationManager.ConnectionStrings[Constants.MongoDB.MongoDBConectionString].ConnectionString;
            string redisConStr = ConfigurationManager.ConnectionStrings[Constants.RedisDB.RedisDBConectionString].ConnectionString;

            // MongoDB name
            string mongoDbName = ConfigurationManager.AppSettings[Constants.MongoDB.MongoDBDatabaseName];
            string logMongoDbName = ConfigurationManager.AppSettings[Constants.MongoDB.LogMongoDBDatabaseName];

            _iUserService = new UserService(mongoConStr, mongoDbName, redisConStr);
            _logService = new LogService(mongoConStr, logMongoDbName);
        }

        // GET: api/User/5
        public IEnumerable<User> Get(string id)
        {
            try
            {
                // Log
                _logService.Add(
                    _logService.CreateLogModel(DateTime.Now,
                    "GET",
                    "User GET",
                    LogType.Information));

                if (string.IsNullOrEmpty(id))
                {
                    return new List<User>();
                }

                var obj = _iUserService.Get(id);
                return obj;
            }
            catch (InternalServerErrorException ex)
            {
                // Log
                _logService.Add(
                    _logService.CreateLogModel(DateTime.Now,
                    "GET",
                    $"Internal server error, {ex.Message} {Environment.NewLine}, Inner: {ex.InnerException} {Environment.NewLine}, StackTrace: {ex.StackTrace}",
                    LogType.Exception));

                return new List<User>();
            }
            catch (Exception ex)
            {
                // Log
                _logService.Add(
                    _logService.CreateLogModel(DateTime.Now,
                    "GET",
                    $"Exception error, {ex.Message} {Environment.NewLine}, Inner: {ex.InnerException} {Environment.NewLine}, StackTrace: {ex.StackTrace}",
                    LogType.Exception));

                return new List<User>();
            }
        }

        // POST: api/User
        public IHttpActionResult Post([FromBody]User value)
        {
            try
            {
                // Log
                _logService.Add(
                    _logService.CreateLogModel(DateTime.Now,
                    "POST",
                    "User POST",
                    LogType.Information));

                // Validation
                if (!ModelState.IsValid)
                {
                    string messages = string.Join(Environment.NewLine, ModelState.Values
                                            .SelectMany(x => x.Errors)
                                            .Select(x => x.ErrorMessage));
                    return BadRequest(messages);
                }

                // Call
                _iUserService.Add(value);

                // Ret
                return Ok();
            }
            catch (InternalServerErrorException ex)
            {
                // Log
                _logService.Add(
                    _logService.CreateLogModel(DateTime.Now,
                    "POST",
                    $"Internal server error, {ex.Message} {Environment.NewLine}, Inner: {ex.InnerException} {Environment.NewLine}, StackTrace: {ex.StackTrace}",
                    LogType.Exception));

                return InternalServerError();
            }
            catch (Exception ex)
            {
                // Log
                _logService.Add(
                    _logService.CreateLogModel(DateTime.Now,
                    "POST",
                    $"Exception error, {ex.Message} {Environment.NewLine}, Inner: {ex.InnerException} {Environment.NewLine}, StackTrace: {ex.StackTrace}",
                    LogType.Exception));

                return InternalServerError();
            }
        }

        // PUT: api/User/5
        public IHttpActionResult Put(string id, [FromBody]User value)
        {
            try
            {
                // Log
                _logService.Add(
                    _logService.CreateLogModel(DateTime.Now,
                    "PUT",
                    "User PUT",
                    LogType.Information));

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
                _iUserService.Update(value);

                // Ret
                return Ok();
            }
            catch (InternalServerErrorException ex)
            {
                // Log
                _logService.Add(
                    _logService.CreateLogModel(DateTime.Now,
                    "PUT",
                    $"Internal server error, {ex.Message} {Environment.NewLine}, Inner: {ex.InnerException} {Environment.NewLine}, StackTrace: {ex.StackTrace}",
                    LogType.Exception));

                return InternalServerError();
            }
            catch (Exception ex)
            {
                // Log
                _logService.Add(
                    _logService.CreateLogModel(DateTime.Now,
                    "PUT",
                    $"Exception error, {ex.Message} {Environment.NewLine}, Inner: {ex.InnerException} {Environment.NewLine}, StackTrace: {ex.StackTrace}",
                    LogType.Exception));

                return InternalServerError();
            }
        }

        // DELETE: api/User/5
        public IHttpActionResult Delete(string id)
        {
            try
            {
                // Log
                _logService.Add(
                    _logService.CreateLogModel(DateTime.Now,
                    "DELETE",
                    "User DELETE",
                    LogType.Information));

                // Validation
                if (string.IsNullOrEmpty(id))
                {
                    return BadRequest("Id is empty");
                }

                // Call
                _iUserService.Delete(id);

                // Ret
                return Ok();
            }
            catch (InternalServerErrorException ex)
            {
                // Log
                _logService.Add(
                    _logService.CreateLogModel(DateTime.Now,
                    "DELETE",
                    $"Internal server error, {ex.Message} {Environment.NewLine}, Inner: {ex.InnerException} {Environment.NewLine}, StackTrace: {ex.StackTrace}",
                    LogType.Exception));

                return InternalServerError();
            }
            catch (Exception ex)
            {
                // Log
                _logService.Add(
                    _logService.CreateLogModel(DateTime.Now,
                    "DELETE",
                    $"Exception error, {ex.Message} {Environment.NewLine}, Inner: {ex.InnerException} {Environment.NewLine}, StackTrace: {ex.StackTrace}",
                    LogType.Exception));

                return InternalServerError();
            }
        }
    }
}
