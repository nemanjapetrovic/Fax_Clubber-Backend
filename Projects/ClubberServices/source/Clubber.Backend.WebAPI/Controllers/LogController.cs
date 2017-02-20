using Clubber.Backend.Services.Logic.LogServices;
using Clubber.Backend.WebAPI.Helpers;
using Clubber.Common.Exceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;
using LogModel = Clubber.Backend.Models.LogModels.Log;

namespace Clubber.Backend.WebAPI.Controllers
{
    public class LogController : ApiController
    {
        private readonly ILogService _logService;

        public LogController()
        {
            // Connection strings
            string mongoConStr = ConfigurationManager.ConnectionStrings[Constants.MongoDB.MongoDBConectionString].ConnectionString;
            // MongoDB name
            string logMongoDbName = ConfigurationManager.AppSettings[Constants.MongoDB.LogMongoDBDatabaseName];

            _logService = new LogService(mongoConStr, logMongoDbName);
        }

        // GET: api/Log/5
        public IEnumerable<LogModel> Get(string skip, string limit)
        {
            try
            {
                if (string.IsNullOrEmpty(skip) || string.IsNullOrEmpty(limit))
                {
                    return new List<LogModel>();
                }

                var logs = _logService.Get(int.Parse(skip), int.Parse(limit));
                return logs;
            }
            catch (InternalServerErrorException ex)
            {
                return new List<LogModel>();
            }
            catch (Exception ex)
            {
                return new List<LogModel>();
            }
        }

        // GET: api/Log/5
        public IEnumerable<LogModel> Get(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return new List<LogModel>();
                }

                var logs = _logService.Get(id);
                return logs;
            }
            catch (InternalServerErrorException ex)
            {
                return new List<LogModel>();
            }
            catch (Exception ex)
            {
                return new List<LogModel>();
            }
        }
    }
}
