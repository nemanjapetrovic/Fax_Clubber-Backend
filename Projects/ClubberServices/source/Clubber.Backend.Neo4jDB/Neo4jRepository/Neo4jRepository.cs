using System.Linq;
using System;
using Clubber.Backend.Neo4jDB.Models;
using Clubber.Backend.Neo4jDB.DependencyInjectionContainer;
using System.Collections.Generic;

namespace Clubber.Backend.Neo4jDB.Neo4jRepository
{
    public class Neo4jRepository : INeo4jRepository
    {
        /// <summary>
        /// Creating client for neo4j db.
        /// </summary>
        /// <param name="connectionString">ConnectionString for neo4j database.</param>
        public Neo4jRepository(string connectionString, string username, string password)
        {
            DependencyContainer.Instance
                .Neo4jClient(connectionString, username, password)
                .Connect();

            // Validate the connection
            IsConnected();
        }

        /// <summary>
        /// Validation for client connectivity.
        /// </summary>
        private void IsConnected()
        {
            if (!DependencyContainer.Instance.Neo4jClient().IsConnected)
            {
                throw new Exception("Neo4j client is not connected!");
            }
        }

        /// <summary>
        /// Used to create a new node in the neo4j database.
        /// This will create a node only if there is no existing one in database.
        /// </summary>
        /// <param name="id">MongoDB _id value.</param>
        /// <param name="nodeLabel">Type of the node.</param>
        public void AddNode(string nodeLabel, string id)
        {
            var newNode = new NodeModel() { _id = id, _nodeType = nodeLabel };
            DependencyContainer.Instance
                .Neo4jClient().Cypher
                .Merge($"(n:{nodeLabel} {{_id: {{newNode}}._id}})")
                .OnCreate()
                .Set($"n = {{newNode}}")
                .WithParams(new
                {
                    id = newNode._id,
                    newNode
                })
                .ExecuteWithoutResults();
        }

        /// <summary>
        /// Used to create a new relationship based on a realtionshipTypeKey.
        /// </summary>
        /// <param name="relationshipTypeKey">Type of the relationship.</param>
        /// <param name="startNodeLabel">Type of the start node.</param>
        /// <param name="endNodeLabel">Type of the end node.</param>
        /// <param name="idBeginUser">Relationship direction from this node.</param>
        /// <param name="idEndUser">End of the relationship. End direction.</param>
        public void AddRelationship(string relationshipTypeKey, string startNodeLabel,
                                string endNodeLabel, string idBeginNode, string idEndNode)
        {
            DependencyContainer.Instance
                .Neo4jClient().Cypher
                .Match($"(n1:{startNodeLabel})", $"(n2:{endNodeLabel})")
                .Where((NodeModel n1) => n1._id == idBeginNode)
                .AndWhere((NodeModel n2) => n2._id == idEndNode)
                .Create($"(n1)-[:{relationshipTypeKey}]->(n2)")
                .ExecuteWithoutResults();
        }

        /// <summary>
        /// Used to get a node from neo4j database.
        /// Will return null if the node doesn't exists.
        /// </summary>
        /// <param name="nodeLabel">Type of a node.</param>
        /// <param name="id">MongoDB _id, we get a node with this id.</param>
        /// <returns>Node that is stored in a database with _id == id value. Will return null if the node doesn't exists.</returns>
        public string GetNode(string nodeLabel, string id)
        {
            var node = DependencyContainer.Instance
                .Neo4jClient().Cypher
                .Match($"(n:{nodeLabel})")
                .Where((NodeModel n) => n._id == id)
                .Return(n => n.As<NodeModel>())
                .Results
                .SingleOrDefault();

            if (node == null)
            {
                return null;
            }

            return node._id;
        }

        /// <summary>
        /// 
        /// </summary>        
        /// <param name="relationshipTypeKey">Type of the relationship.</param>
        /// <param name="startNodeLabel">Type of the start node.</param>
        /// <param name="idBeginUser">Relationship direction from this node.</param>
        /// <returns></returns>
        public IList<string> GetNodesByRelationship(string relationshipTypeKey, string startNodeLabel, string idBeginNode)
        {
            var nodes = DependencyContainer.Instance
                .Neo4jClient().Cypher
                .Match($"(n1)-[{relationshipTypeKey}]->(n2)")
                .Where((NodeModel n1) => n1._id == idBeginNode)
                .Return<NodeModel>("n2")
                .Results;

            if (nodes == null)
            {
                return null;
            }

            return nodes.Select(item => item._id).ToList();
        }

        /// <summary>
        /// Used to remove only one node from db where node _id == id.
        /// </summary>
        /// <param name="nodeLabel">Type of a node.</param>
        /// <param name="id">MongoDB _id value.</param>
        public void RemoveNodeAndItsRelationships(string nodeLabel, string id)
        {
            DependencyContainer.Instance
                .Neo4jClient().Cypher
                .Match($"(n:{nodeLabel})")
                    .Where((NodeModel n) => n._id == id)
                    .DetachDelete("n")
                    .ExecuteWithoutResults();
        }

        /// <summary>
        /// Used to remove relationship between two nodes.
        /// </summary>
        /// <param name="relationshipTypeKey">Type of the relationship.</param>
        /// <param name="startNodeLabel">Type of the start node.</param>
        /// <param name="endNodeLabel">Type of the end node.</param>
        /// <param name="idBeginUser">Relationship direction from this node.</param>
        /// <param name="idEndUser">End of the relationship. End direction.</param>
        public void RemoveRelationship(string relationshipTypeKey, string startNodeLabel,
                                string endNodeLabel, string idBeginNode, string idEndNode)
        {
            DependencyContainer.Instance
                .Neo4jClient().Cypher
                .Match($"(n1)-[{relationshipTypeKey}]->(n2)")
                .Where((NodeModel n1) => n1._id == idBeginNode)
                .AndWhere((NodeModel n2) => n2._id == idEndNode)
                .Delete($"{relationshipTypeKey}")
                .ExecuteWithoutResults();
        }

    }
}
