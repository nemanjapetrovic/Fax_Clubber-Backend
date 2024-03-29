﻿using Clubber.Backend.Models.DomainModels;
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

namespace Clubber.WebAPI.Controllers
{
    public class ClubController : ApiController
    {
        private readonly IService<Club> _iClubService;
        private readonly ILogService _logService;

        public ClubController()
        {
            // Connection strings
            string mongoConStr = ConfigurationManager.ConnectionStrings[Constants.MongoDB.MongoDBConectionString].ConnectionString;
            string redisConStr = ConfigurationManager.ConnectionStrings[Constants.RedisDB.RedisDBConectionString].ConnectionString;

            // MongoDB name
            string mongoDbName = ConfigurationManager.AppSettings[Constants.MongoDB.MongoDBDatabaseName];
            string logMongoDbName = ConfigurationManager.AppSettings[Constants.MongoDB.LogMongoDBDatabaseName];

            _iClubService = new ClubService(mongoConStr, mongoDbName, redisConStr);
            _logService = new LogService(mongoConStr, logMongoDbName);
        }

        // GET: api/Club/5        
        public IEnumerable<Club> Get(string name)
        {
            try
            {
                // Log
                _logService.Add(
                    _logService.CreateLogModel(DateTime.Now,
                    "GET",
                    "Club GET",
                    LogType.Information));

                if (string.IsNullOrEmpty(name))
                {
                    return new List<Club>();
                }

                var objs = _iClubService.Get(Regex.Replace(name, @"\s+", "").ToLower());
                return objs;
            }
            catch (InternalServerErrorException ex)
            {
                // Log
                _logService.Add(
                    _logService.CreateLogModel(DateTime.Now,
                    "GET",
                    $"Internal server error, {ex.Message} {Environment.NewLine}, Inner: {ex.InnerException} {Environment.NewLine}, StackTrace: {ex.StackTrace}",
                    LogType.Exception));

                return new List<Club>();
            }
            catch (Exception ex)
            {
                // Log
                _logService.Add(
                    _logService.CreateLogModel(DateTime.Now,
                    "GET",
                    $"Exception error, {ex.Message} {Environment.NewLine}, Inner: {ex.InnerException} {Environment.NewLine}, StackTrace: {ex.StackTrace}",
                    LogType.Exception));

                return new List<Club>();
            }
        }

        // POST: api/Club
        public IHttpActionResult Post([FromBody]Club value)
        {
            try
            {
                // Log
                _logService.Add(
                    _logService.CreateLogModel(DateTime.Now,
                    "POST",
                    "Club POST",
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
                _iClubService.Add(value);

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

        // PUT: api/Club/5
        public IHttpActionResult Put(string id, [FromBody]Club value)
        {
            try
            {
                // Log
                _logService.Add(
                    _logService.CreateLogModel(DateTime.Now,
                    "PUT",
                    "Club PUT",
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
                _iClubService.Update(value);

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

        // DELETE: api/Club/5
        public IHttpActionResult Delete(string id)
        {
            try
            {
                // Log
                _logService.Add(
                    _logService.CreateLogModel(DateTime.Now,
                    "DELETE",
                    "Club DELETE",
                    LogType.Information));

                // Validation
                if (string.IsNullOrEmpty(id))
                {
                    return BadRequest("Id is empty");
                }

                // Call
                _iClubService.Delete(id);

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
