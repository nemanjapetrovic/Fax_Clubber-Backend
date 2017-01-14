using Neo4jClient;
using System.Linq;
using System;
using Clubber.Backend.Neo4jDB.Models;

namespace Clubber.Backend.Neo4jDB.Neo4jRepository
{
    public class Neo4jRepository : INeo4jRepository
    {
        private GraphClient client = null;

        /// <summary>
        /// Creating client for neo4j db.
        /// </summary>
        /// <param name="connectionString">ConnectionString for neo4j database.</param>
        public Neo4jRepository(string connectionString)
        {
            client = new GraphClient(new Uri(connectionString));
            client.Connect();

            IsConnected();
        }

        /// <summary>
        /// Validation for client connectivity.
        /// </summary>
        private void IsConnected()
        {
            if (!client.IsConnected)
            {
                throw new Exception("Neo4j client is not connected!");
            }
        }

        /// <summary>
        /// Used to create a new node in the neo4j database.
        /// </summary>
        /// <param name="id">MongoDB _id value.</param>
        /// <param name="entityType">Type of the node.</param>
        public void AddNode(string entityType, string id)
        {
            IsConnected();

            var newNode = new NodeModel() { _id = id, _nodeType = entityType };
            client.Cypher
                .Create($"(n:{entityType} {entityType})")
                .WithParam($"{entityType}", newNode)
                .ExecuteWithoutResults();
        }

        /// <summary>
        /// Used to create a new relationship based on a realtionshipTypeKey.
        /// </summary>
        /// <param name="relationshipTypeKey">Type of the relationship.</param>
        /// <param name="entityType">Type of the node.</param>
        /// <param name="idBeginUser">Relationship direction from this node.</param>
        /// <param name="idEndUser">End of the relationship. End direction.</param>
        public void AddRelationship(string relationshipTypeKey, string entityType, string idBeginUser, string idEndUser)
        {
            IsConnected();

            client.Cypher
                .Match($"(n1:{entityType})", $"(n2:{entityType})")
                .Where((NodeModel node1) => node1._id.Equals(idBeginUser))
                .AndWhere((NodeModel node2) => node2._id.Equals(idEndUser))
                .Create($"n1-[:{relationshipTypeKey}]->n2")
                .ExecuteWithoutResults();
        }

        /// <summary>
        /// Used to get a node from neo4j database.
        /// </summary>
        /// <param name="entityType">Type of a node.</param>
        /// <param name="id">MongoDB _id, we get a node with this id.</param>
        /// <returns>Node that is stored in a database with _id == id value.</returns>
        public NodeModel GetNode(string entityType, string id)
        {
            IsConnected();

            var node = client.Cypher
                .Match($"(n:{entityType})")
                .Where((NodeModel nodeModel) => nodeModel._id.Equals(id))
                .Return(nodeModel => nodeModel.As<NodeModel>())
                .Results
                .SingleOrDefault();

            return node;
        }

        /// <summary>
        /// Used to remove only one node from db where node _id == id.
        /// </summary>
        /// <param name="entityType">Type of a node.</param>
        /// <param name="id">MongoDB _id value.</param>
        public void RemoveNode(string entityType, string id)
        {
            IsConnected();

            client.Cypher
                .Match($"(n:{entityType})")
                    .Where((NodeModel nodeModel) => nodeModel._id.Equals(id))
                    .Delete("n")
                    .ExecuteWithoutResults();
        }

        /// <summary>
        /// Used to remove node and it's all inbound relationships where node _id == id.
        /// </summary>
        /// <param name="relationshipTypeKey">Type of a relationship.</param>
        /// <param name="entityType">Type of a node.</param>
        /// <param name="id">MongoDB _id value</param>
        public void RemoveNodeAndRelationship(string relationshipTypeKey, string entityType, string id)
        {
            IsConnected();

            client.Cypher
                .OptionalMatch($"(n:{entityType})<-[{relationshipTypeKey}]-()")
                    .Where((NodeModel nodeModel) => nodeModel._id.Equals(id))
                    .Delete($"{relationshipTypeKey}, n")
                    .ExecuteWithoutResults();
        }
    }
}
