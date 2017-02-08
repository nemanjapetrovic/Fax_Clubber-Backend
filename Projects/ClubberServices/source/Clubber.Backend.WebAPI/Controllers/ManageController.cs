﻿using Clubber.Backend.Models.ParameterModels;
using Clubber.Backend.Services.Logic.RelationshipServices;
using Clubber.Backend.WebAPI.Helpers;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;

namespace Clubber.Backend.WebAPI.Controllers
{
    public class ManageController : ApiController
    {
        private readonly string _RelationshipKey = "MANAGE";
        private readonly string _BeginNodeLabel = "Manager";
        private readonly string _EndNodeLabel = "Club";
        private readonly IRelationshipServices _relationshipService;

        public ManageController()
        {
            string conString = ConfigurationManager.ConnectionStrings[Constants.Neo4jDB.Neo4jDBConnectionString].ConnectionString;
            string username = ConfigurationManager.AppSettings[Constants.Neo4jDB.Neo4jDBUsername];
            string password = ConfigurationManager.AppSettings[Constants.Neo4jDB.Neo4jDBPassword];

            _relationshipService = new RelationshipService(conString,
                                                           username,
                                                           password);
        }

        // GET: api/Manage/5
        public IEnumerable<string> Get(string id)
        {
            var items = _relationshipService.GetElementsInRelationshipWith(
                _RelationshipKey,
                _BeginNodeLabel,
                id);

            return items;
        }

        // POST: api/Manage
        public void Post([FromBody]NodeData nodeData)
        {
            _relationshipService.CreateRelationship(
                _RelationshipKey,
                _BeginNodeLabel,
                _EndNodeLabel,
                nodeData.idNodeBegin,
                nodeData.idNodeEnd);
        }

        // DELETE: api/Manage/5
        public void Delete([FromBody]NodeData nodeData)
        {
            _relationshipService.RemoveRelationship(
                _RelationshipKey,
                _BeginNodeLabel,
                _EndNodeLabel,
                nodeData.idNodeBegin,
                nodeData.idNodeEnd);
        }

    }
}