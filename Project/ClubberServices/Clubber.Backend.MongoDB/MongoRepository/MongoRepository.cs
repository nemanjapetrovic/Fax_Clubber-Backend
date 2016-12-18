using System;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Clubber.Backend.MongoDB.MongoRepository
{
    internal class MongoRepository<T> : IMongoRepository<T> where T : class
    {
        private IMongoDatabase _database;
        private IMongoCollection<T> _collection;
        private string _tableName;

        /// <summary>
        /// Sets the database, database table name and gets collection by database table name.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="tableName"></param>
        public MongoRepository(IMongoDatabase db, string tableName)
        {
            _database = db;
            _tableName = tableName;
            _collection = _database.GetCollection<T>(_tableName);
        }

        /// <summary>
        /// Get all entities.
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> Get()
        {
            var documents = _collection.AsQueryable<T>();
            return documents;
        }

        /// <summary>
        /// Get entity by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get(ObjectId id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            return (T)_collection.Find<T>(filter);
        }

        /// <summary>
        /// Add a new entity to collection.
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity)
        {
            _collection.InsertOne(entity);
        }

        /// <summary>
        /// Update entity in collection
        /// </summary>
        /// <param name="queryExpression"></param>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        public void Update(Expression<Func<T, ObjectId>> queryExpression, ObjectId id, T entity)
        {
            var query = Builders<T>.Filter.Eq(queryExpression, id);
            _collection.UpdateOne(query, new ObjectUpdateDefinition<T>(entity));
        }

        /// <summary>
        /// Remove entity from collection.
        /// </summary>
        /// <param name="queryExpression"></param>
        /// <param name="id"></param>
        public void Delete(Expression<Func<T, ObjectId>> queryExpression, ObjectId id)
        {
            var query = Builders<T>.Filter.Eq(queryExpression, id);
            _collection.DeleteOne(query);
        }
    }
}
