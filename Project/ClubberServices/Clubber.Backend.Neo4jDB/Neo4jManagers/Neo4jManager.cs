using Clubber.Backend.Neo4jDB.Neo4jRepository;
using System;

namespace Clubber.Backend.Neo4jDB.Neo4jManagers
{
    public class Neo4jManager
    {
        private string _connectionString;
        private string _username;
        private string _password;

        protected INeo4jRepository _neoRepo = null;
        public INeo4jRepository Neo4jRepository
        {
            get
            {
                if (_neoRepo == null)
                {
                    _neoRepo = new Neo4jRepository.Neo4jRepository(_connectionString, _username, _password);
                }
                return _neoRepo;
            }
        }

        public Neo4jManager(string connectionString, string username, string password)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("Neo4j connection string is empty!");
            }

            if (string.IsNullOrEmpty(username))
            {
                throw new Exception("Neo4j username string is empty!");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new Exception("Neo4j password string is empty!");
            }

            _connectionString = connectionString;
            _username = username;
            _password = password;
        }
    }
}
