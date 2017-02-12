using Clubber.Backend.Models.ParameterModels;
using Clubber.Backend.Services.Logic.RelationshipServices;
using Clubber.Backend.WebAPI.Helpers;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;
using System.Linq;
using System;
using Clubber.Common.Exceptions.Exceptions;

namespace Clubber.Backend.WebAPI.Controllers
{
    public class FollowController : ApiController
    {
        private readonly string _RelationshipKey = "FOLLOW";
        private readonly string _BeginNodeLabel = "User";
        private readonly string _EndNodeLabel = "Club";
        private readonly IRelationshipServices _relationshipService;

        public FollowController()
        {
            string conString = ConfigurationManager.ConnectionStrings[Constants.Neo4jDB.Neo4jDBConnectionString].ConnectionString;
            string username = ConfigurationManager.AppSettings[Constants.Neo4jDB.Neo4jDBUsername];
            string password = ConfigurationManager.AppSettings[Constants.Neo4jDB.Neo4jDBPassword];

            _relationshipService = new RelationshipService(conString,
                                                           username,
                                                           password);
        }

        // GET: api/Follow/5
        public IEnumerable<string> Get(string id)
        {
            try
            {
                var items = _relationshipService.GetElementsInRelationshipWith(
                _RelationshipKey,
                _BeginNodeLabel,
                _EndNodeLabel,
                id);

                return items;
            }
            catch (InternalServerErrorException ex)
            {
                return new List<string>();
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }

        // POST: api/Follow
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
                return InternalServerError();
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        // DELETE: api/Follow/5
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
                return InternalServerError();
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }
    }
}