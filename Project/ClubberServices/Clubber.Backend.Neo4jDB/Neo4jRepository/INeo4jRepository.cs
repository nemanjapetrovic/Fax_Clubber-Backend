using Clubber.Backend.Neo4jDB.Models;
using Neo4jClient;

namespace Clubber.Backend.Neo4jDB.Neo4jRepository
{
    public interface INeo4jRepository
    {
        NodeReference AddNode(string entityType, string id);
        void RemoveNode(string id);
        RelationshipReference AddRelationship(string relationshipTypeKey, NodeReference<string> beginNode, NodeReference<string> endNode);
        void RemoveRelationship(string relationshipTypeKey);
        NodeModel GetNode(string entityType, string id);
    }
}
