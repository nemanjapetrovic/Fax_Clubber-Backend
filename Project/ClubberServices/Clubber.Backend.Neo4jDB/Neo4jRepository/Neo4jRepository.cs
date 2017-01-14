using Clubber.Backend.Neo4jDB.Relationships;
using System.Globalization;
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
        /// <returns></returns>
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
        /// <returns>NodeReference to a created node.</returns>
        public NodeReference AddNode(string entityType, string id)
        {
            IsConnected();

            //Creating new node in database
            var refEntity = client.Create(new NodeModel { _id = id, _nodeType = entityType });
            return refEntity;
        }

        /// <summary>
        /// Used to create a new relationship based on a realtionshipTypeKey.
        /// </summary>
        /// <param name="relationshipTypeKey">Type of the relationship.</param>
        /// <param name="beginNode">Relationship direction from this node.</param>
        /// <param name="endNode">End of the relationship. End direction.</param>
        /// <returns>RelationshipReference to a created relationship.</returns>
        public RelationshipReference AddRelationship(string relationshipTypeKey, NodeReference<string> beginNode, NodeReference<string> endNode)
        {
            IsConnected();

            RelationshipReference refRelationship = null;
            //Creating new relationship in database            
            switch (relationshipTypeKey)
            {
                case Constants.Neo4jTypeKeys.FollowRelationship:
                    refRelationship = client.CreateRelationship(beginNode, new FollowRelationship(endNode));
                    break;
                case Constants.Neo4jTypeKeys.GoingRelationship:
                    refRelationship = client.CreateRelationship(beginNode, new GoingRelationship(endNode));
                    break;
                case Constants.Neo4jTypeKeys.ManageRelationship:
                    refRelationship = client.CreateRelationship(beginNode, new ManageRelationship(endNode));
                    break;
                case Constants.Neo4jTypeKeys.PlaceRelationship:
                    refRelationship = client.CreateRelationship(beginNode, new PlaceRelationship(endNode));
                    break;
            }

            return refRelationship;
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

            // Convert to lower case
            entityType = entityType.ToLower();
            // Upper case only first letter in a string
            string entityTypeUpper = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(entityType);

            // Get node item
            var node = client.Cypher
                .Match($"({entityType}:{entityTypeUpper})")
                .Where((NodeModel nodeModel) => nodeModel._id.Equals(id))
                .Return(nodeModel => nodeModel.As<NodeModel>())
                .Results
                .SingleOrDefault();

            return node;
        }

        public void RemoveNode(string id)
        {
            IsConnected();
            throw new NotImplementedException();
        }

        public void RemoveRelationship(string relationshipTypeKey)
        {
            IsConnected();
            throw new NotImplementedException();
        }


    }
}
