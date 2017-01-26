using Clubber.Backend.Neo4jDB.Models;
using System.Collections.Generic;

namespace Clubber.Backend.Neo4jDB.Neo4jRepository
{
    /// <summary>
    /// Used to store data with relationships. Data will only contain _id hashes from Mongo.
    /// </summary>
    public interface INeo4jRepository
    {
        void AddNode(string nodeLabel, string id);
        void RemoveNodeAndItsRelationships(string nodeLabel, string id);

        void AddRelationship(string relationshipTypeKey, string startNodeLabel, string endNodeLabel, string idBeginNode, string idEndNode);
        void RemoveRelationship(string relationshipTypeKey, string startNodeLabel, string endNodeLabel, string idBeginNode, string idEndNode);

        string GetNode(string nodeLabel, string id);
        IList<string> GetNodesByRelationship(string relationshipTypeKey, string startNodeLabel, string idBeginNode);
    }
}
