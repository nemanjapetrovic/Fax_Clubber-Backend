using Clubber.Backend.Neo4jDB.Neo4jManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clubber.Backend.Services.Logic.RelationshipServices
{
    /// <summary>
    /// Relationships:
    /// USER - FOLLOW -> CLUB
    /// USER - GOING -> EVENT
    /// MANAGER - MANAGE -> CLUB
    /// EVENT - PLACE -> CLUB
    /// </summary>
    public class RelationshipService : IRelationshipServices
    {
        private readonly Neo4jManager _neoManager;

        public RelationshipService(string connectionString,
                             string username,
                             string password)
        {
            _neoManager = new Neo4jManager(connectionString, username, password);
        }

        public void CreateRelationship(string relationshipKey, string beginNodeLabel, string endNodeLabel, string beginNodeId, string endNodeId)
        {
            _neoManager.Neo4jRepository.AddNode(beginNodeLabel, beginNodeId);
            _neoManager.Neo4jRepository.AddNode(endNodeLabel, endNodeId);
            _neoManager.Neo4jRepository.AddRelationship(relationshipKey, beginNodeLabel, endNodeLabel, beginNodeId, endNodeId);
        }

        public IList<string> GetElementsInRelationshipWith(string relationshipKey, string beginNodeLabel, string beginNodeId)
        {
            return _neoManager.Neo4jRepository.GetNodesByRelationship(relationshipKey, beginNodeLabel, beginNodeId);
        }

        public void RemoveRelationship(string relationshipKey, string beginNodeLabel, string endNodeLabel, string beginNodeId, string endNodeId)
        {
            _neoManager.Neo4jRepository.RemoveRelationship(relationshipKey, beginNodeLabel, endNodeLabel, beginNodeId, endNodeId);
        }
    }
}
