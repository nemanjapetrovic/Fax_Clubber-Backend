using Microsoft.VisualStudio.TestTools.UnitTesting;
using Clubber.Backend.Neo4jDB.Neo4jManagers;

namespace Clubber.Backend.Neo4jDB.Test
{
    [TestClass]
    public class Neo4jRepositoryTest
    {
        private Neo4jManager _neo = new Neo4jManager("http://localhost:7474/db/data", "neo4j", "nemanja94nemac94");
        private string nodeId1 = "hash-1-hash-test";
        private string nodeId2 = "hash-2-hash-test";
        private string nodeId3 = "hash-3-hash-test";
        private string nodeLabel = "lblTest";
        private string relationship = "RELATIONSHIPTMP";

        [TestMethod]
        public void AddNode()
        {
            _neo.Neo4jRepository.AddNode(nodeLabel, nodeId1);
            _neo.Neo4jRepository.AddNode(nodeLabel, nodeId2);

            var ret1 = _neo.Neo4jRepository.GetNode(nodeLabel, nodeId1);
            Assert.IsNotNull(ret1);

            var ret2 = _neo.Neo4jRepository.GetNode(nodeLabel, nodeId2);
            Assert.IsNotNull(ret2);

            if (!ret1.Equals(nodeId1))
            {
                Assert.Fail();
            }


            if (!ret2.Equals(nodeId2))
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void RemoveNode()
        {
            _neo.Neo4jRepository.RemoveNodeAndItsRelationships(nodeLabel, nodeId2);
            var nodeIdTmp = _neo.Neo4jRepository.GetNode(nodeLabel, nodeId2);
        }

        [TestMethod]
        public void AddRelationship()
        {
            _neo.Neo4jRepository.AddNode(nodeLabel, nodeId1);
            _neo.Neo4jRepository.AddNode(nodeLabel, nodeId3);

            var ret1 = _neo.Neo4jRepository.GetNode(nodeLabel, nodeId1);
            Assert.IsNotNull(ret1);

            var ret2 = _neo.Neo4jRepository.GetNode(nodeLabel, nodeId3);
            Assert.IsNotNull(ret2);

            if (!ret1.Equals(nodeId1))
            {
                Assert.Fail();
            }


            if (!ret2.Equals(nodeId3))
            {
                Assert.Fail();
            }

            _neo.Neo4jRepository.AddRelationship(relationship, nodeLabel, nodeLabel, nodeId1, nodeId3);

            var relNode = _neo.Neo4jRepository.GetNodesByRelationship(relationship, nodeLabel, nodeId1);

            Assert.IsNotNull(relNode);

            if (relNode != null && !relNode.Contains(ret2))
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void RemoveRelationship()
        {
            _neo.Neo4jRepository.RemoveRelationship(relationship, nodeLabel, nodeLabel, nodeId1, nodeId3);

            var relNode = _neo.Neo4jRepository.GetNodesByRelationship(relationship, nodeLabel, nodeId1);
        }

        [TestMethod]
        public void GetNode()
        {
            _neo.Neo4jRepository.AddNode(nodeLabel, nodeId1);
            _neo.Neo4jRepository.AddNode(nodeLabel, nodeId2);

            var ret1 = _neo.Neo4jRepository.GetNode(nodeLabel, nodeId1);
            Assert.IsNotNull(ret1);

            var ret2 = _neo.Neo4jRepository.GetNode(nodeLabel, nodeId2);
            Assert.IsNotNull(ret2);

            if (!ret1.Equals(nodeId1))
            {
                Assert.Fail();
            }


            if (!ret2.Equals(nodeId2))
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void GetNodesByRelationship()
        {
            _neo.Neo4jRepository.GetNodesByRelationship(relationship, nodeLabel, nodeId1);
        }

        [TestMethod]
        public void CountRelationshipsEndNodes()
        {
            var data = _neo.Neo4jRepository.CountRelationshipsEndNodes(relationship, nodeLabel, nodeId1);
        }

        [TestMethod]
        public void IsInARelationship()
        {
            var data = _neo.Neo4jRepository.IsInARelationship(nodeId3, true);
        }

    }
}
