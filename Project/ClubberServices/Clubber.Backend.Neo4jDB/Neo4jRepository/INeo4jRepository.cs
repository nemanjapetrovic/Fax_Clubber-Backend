using Clubber.Backend.Neo4jDB.Models;

namespace Clubber.Backend.Neo4jDB.Neo4jRepository
{
    /// <summary>
    /// Used to store data with relationships. Data will only contain _id hashes from Mongo.
    /// </summary>
    public interface INeo4jRepository
    {
        void AddNode(string nodeLabel, string id);
        void RemoveNode(string nodeLabel, string id);
        void AddRelationship(string relationshipTypeKey, string nodeLabel, string idBeginUser, string idEndUser);
        void RemoveNodeAndRelationship(string relationshipTypeKey, string nodeLabel, string id);
        NodeModel GetNode(string nodeLabel, string id);
    }
}
