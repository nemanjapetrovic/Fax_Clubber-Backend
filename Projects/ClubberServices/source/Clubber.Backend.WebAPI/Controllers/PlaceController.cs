using Clubber.Backend.Models.LogModels;
using Clubber.Backend.Models.ParameterModels;
using Clubber.Backend.Services.Logic.LogServices;
using Clubber.Backend.Services.Logic.RelationshipServices;
using Clubber.Backend.WebAPI.Helpers;
using Clubber.Common.Exceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;

namespace Clubber.Backend.WebAPI.Controllers
{
    public class PlaceController : ApiController
    {
        private readonly string _RelationshipKey = "PLACE";
        private readonly string _BeginNodeLabel = "Event";
        private readonly string _EndNodeLabel = "Club";

        private readonly IRelationshipServices _relationshipService;
        private readonly ILogService _logService;

        public PlaceController()
        {
            string neo4jConnStr = ConfigurationManager.ConnectionStrings[Constants.Neo4jDB.Neo4jDBConnectionString].ConnectionString;
            string username = ConfigurationManager.AppSettings[Constants.Neo4jDB.Neo4jDBUsername];
            string password = ConfigurationManager.AppSettings[Constants.Neo4jDB.Neo4jDBPassword];

            // Connection strings
            string mongoConStr = ConfigurationManager.ConnectionStrings[Constants.MongoDB.MongoDBConectionString].ConnectionString;
            // MongoDB name
            string logMongoDbName = ConfigurationManager.AppSettings[Constants.MongoDB.LogMongoDBDatabaseName];

            _relationshipService = new RelationshipService(neo4jConnStr, username, password);
            _logService = new LogService(mongoConStr, logMongoDbName);
        }

        // GET: api/Place/5
        public IEnumerable<string> Get(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return new List<string>();
                }

                var items = _relationshipService.GetElementsInRelationshipWith(
                    _RelationshipKey,
                    _BeginNodeLabel,
                    _EndNodeLabel,
                    id);

                return items;
            }
            catch (InternalServerErrorException ex)
            {
                // Log
                _logService.Add(
                    _logService.CreateLogModel(DateTime.Now,
                    "GET",
                    $"Internal server error, {ex.Message}",
                    LogType.Exception));

                return new List<string>();
            }
            catch (Exception ex)
            {
                // Log
                _logService.Add(
                    _logService.CreateLogModel(DateTime.Now,
                    "GET",
                    $"Exception error, {ex.Message}",
                    LogType.Exception));

                return new List<string>();
            }
        }

        // POST: api/Place
        public IHttpActionResult Post([FromBody]NodeData nodeData)
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
                _relationshipService.CreateRelationship(
                    _RelationshipKey,
                    _BeginNodeLabel,
                    _EndNodeLabel,
                    nodeData.idNodeBegin,
                    nodeData.idNodeEnd);

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

        // DELETE: api/Place/5
        public IHttpActionResult Delete([FromBody]NodeData nodeData)
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
                _relationshipService.RemoveRelationship(
                    _RelationshipKey,
                    _BeginNodeLabel,
                    _EndNodeLabel,
                    nodeData.idNodeBegin,
                    nodeData.idNodeEnd);

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