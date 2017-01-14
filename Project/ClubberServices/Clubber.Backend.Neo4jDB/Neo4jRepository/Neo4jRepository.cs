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
        /// Validation for relationship name. Check if the relValue exists in Constants.Relationships
        /// </summary>
        /// <param name="relValue">Is a name of relationship.</param>
        private void IsRelationshipValid(string relValue)
        {

        }

        /// <summary>
        /// Validation for node label name. Check if the lblValue exists in Constants.NodeLabels
        /// </summary>
        /// <param name="lblValue">Is a name of a node, node type.</param>
        private void IsNodeLabelValid(string lblValue)
        {

        }

        /// <summary>
        /// Used to create a new node in the neo4j database.
        /// </summary>
        /// <param name="id">MongoDB _id value.</param>
        /// <param name="nodeLabel">Type of the node.</param>
        public void AddNode(string nodeLabel, string id)
        {
            IsConnected();

            var newNode = new NodeModel() { _id = id, _nodeType = nodeLabel };
            client.Cypher
                .Create($"(n:{nodeLabel} {nodeLabel})")
                .WithParam($"{nodeLabel}", newNode)
                .ExecuteWithoutResults();
        }

        /// <summary>
        /// Used to create a new relationship based on a realtionshipTypeKey.
        /// </summary>
        /// <param name="relationshipTypeKey">Type of the relationship.</param>
        /// <param name="nodeLabel">Type of the node.</param>
        /// <param name="idBeginUser">Relationship direction from this node.</param>
        /// <param name="idEndUser">End of the relationship. End direction.</param>
        public void AddRelationship(string relationshipTypeKey, string nodeLabel, string idBeginUser, string idEndUser)
        {
            IsConnected();

            client.Cypher
                .Match($"(n1:{nodeLabel})", $"(n2:{nodeLabel})")
                .Where((NodeModel node1) => node1._id.Equals(idBeginUser))
                .AndWhere((NodeModel node2) => node2._id.Equals(idEndUser))
                .Create($"n1-[:{relationshipTypeKey}]->n2")
                .ExecuteWithoutResults();
        }

        /// <summary>
        /// Used to get a node from neo4j database.
        /// </summary>
        /// <param name="nodeLabel">Type of a node.</param>
        /// <param name="id">MongoDB _id, we get a node with this id.</param>
        /// <returns>Node that is stored in a database with _id == id value.</returns>
        public NodeModel GetNode(string nodeLabel, string id)
        {
            IsConnected();

            var node = client.Cypher
                .Match($"(n:{nodeLabel})")
                .Where((NodeModel nodeModel) => nodeModel._id.Equals(id))
                .Return(nodeModel => nodeModel.As<NodeModel>())
                .Results
                .SingleOrDefault();

            return node;
        }

        /// <summary>
        /// Used to remove only one node from db where node _id == id.
        /// </summary>
        /// <param name="nodeLabel">Type of a node.</param>
        /// <param name="id">MongoDB _id value.</param>
        public void RemoveNode(string nodeLabel, string id)
        {
            IsConnected();

            client.Cypher
                .Match($"(n:{nodeLabel})")
                    .Where((NodeModel nodeModel) => nodeModel._id.Equals(id))
                    .Delete("n")
                    .ExecuteWithoutResults();
        }

        /// <summary>
        /// Used to remove node and it's all inbound relationships where node _id == id.
        /// </summary>
        /// <param name="relationshipTypeKey">Type of a relationship.</param>
        /// <param name="nodeLabel">Type of a node.</param>
        /// <param name="id">MongoDB _id value</param>
        public void RemoveNodeAndRelationship(string relationshipTypeKey, string nodeLabel, string id)
        {
            IsConnected();

            client.Cypher
                .OptionalMatch($"(n:{nodeLabel})<-[{relationshipTypeKey}]-()")
                    .Where((NodeModel nodeModel) => nodeModel._id.Equals(id))
                    .Delete($"{relationshipTypeKey}, n")
                    .ExecuteWithoutResults();
        }
    }
}
