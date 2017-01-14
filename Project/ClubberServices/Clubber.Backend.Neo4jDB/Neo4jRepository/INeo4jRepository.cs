using Clubber.Backend.Neo4jDB.Models;

namespace Clubber.Backend.Neo4jDB.Neo4jRepository
{
    /// <summary>
    /// Used to store data with relationships.
    /// </summary>
    public interface INeo4jRepository
    {
        void AddNode(string entityType, string id);
        void RemoveNode(string entityType, string id);
        void AddRelationship(string relationshipTypeKey, string entityType, string idBeginUser, string idEndUser);
        void RemoveNodeAndRelationship(string relationshipTypeKey, string entityType, string id);
        NodeModel GetNode(string entityType, string id);
    }
}
