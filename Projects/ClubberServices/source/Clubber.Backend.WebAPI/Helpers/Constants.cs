namespace Clubber.Backend.WebAPI.Helpers
{
    internal static class Constants
    {
        internal class MongoDB
        {
            internal static string MongoDBConectionString = "MongoDBConectionString";
            internal static string MongoDBDatabaseName = "MongoDBDatabaseName";
        }

        internal class RedisDB
        {
            internal static string RedisDBConectionString = "RedisDBConectionString";
        }

        internal class Neo4jDB
        {
            internal static string Neo4jDBConnectionString = "Neo4jDBConnectionString";
            internal static string Neo4jDBUsername = "Neo4jDBUsername";
            internal static string Neo4jDBPassword = "Neo4jDBPassword";
        }
    }
}