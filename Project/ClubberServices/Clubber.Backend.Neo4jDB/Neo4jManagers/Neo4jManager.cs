using Clubber.Backend.Neo4jDB.Neo4jRepository;
using System;

namespace Clubber.Backend.Neo4jDB.Neo4jManagers
{
    public class Neo4jManager
    {
        private string _connectionString;

        protected INeo4jRepository _neoRepo = null;
        public INeo4jRepository Neo4jRepository
        {
            get
            {
                if (_neoRepo == null)
                {
                    _neoRepo = new Neo4jRepository.Neo4jRepository(_connectionString);
                }
                return _neoRepo;
            }
        }

        public Neo4jManager(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("Neo4j connection string is empty!");
            }

            _connectionString = connectionString;
        }
    }
}
