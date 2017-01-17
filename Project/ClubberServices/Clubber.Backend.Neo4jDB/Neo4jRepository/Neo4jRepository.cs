﻿using System.Linq;
using System;
using Clubber.Backend.Neo4jDB.Models;
using Clubber.Backend.Neo4jDB.DependencyInjectionContainer;

namespace Clubber.Backend.Neo4jDB.Neo4jRepository
{
    public class Neo4jRepository : INeo4jRepository
    {
        /// <summary>
        /// Creating client for neo4j db.
        /// </summary>
        /// <param name="connectionString">ConnectionString for neo4j database.</param>
        public Neo4jRepository(string connectionString)
        {
            DependencyContainer.Instance.Neo4jClient(connectionString).Connect();

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
        /// </summary>
        /// <param name="id">MongoDB _id value.</param>
        /// <param name="nodeLabel">Type of the node.</param>
        public void AddNode(string nodeLabel, string id)
        {
            var newNode = new NodeModel() { _id = id, _nodeType = nodeLabel };
            DependencyContainer.Instance
                .Neo4jClient().Cypher
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
            DependencyContainer.Instance
                .Neo4jClient().Cypher
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
            var node = DependencyContainer.Instance
                .Neo4jClient().Cypher
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
            DependencyContainer.Instance
                .Neo4jClient().Cypher
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
            DependencyContainer.Instance
                .Neo4jClient().Cypher
                .OptionalMatch($"(n:{nodeLabel})<-[{relationshipTypeKey}]-()")
                    .Where((NodeModel nodeModel) => nodeModel._id.Equals(id))
                    .Delete($"{relationshipTypeKey}, n")
                    .ExecuteWithoutResults();
        }
    }
}
