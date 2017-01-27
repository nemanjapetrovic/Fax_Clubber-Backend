using System;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Clubber.Backend.MongoDB.MongoRepository
{
    public class MongoRepository<T> : IMongoRepository<T> where T : class
    {
        private IMongoDatabase _database;
        private IMongoCollection<T> _collection;
        private string _collectionName;

        /// <summary>
        /// Sets the database, database table name and gets collection by database table name.
        /// </summary>
        public MongoRepository(IMongoDatabase db, string collectionName)
        {
            if (string.IsNullOrEmpty(collectionName))
            {
                throw new Exception("Mongo collection name is empty!");
            }

            _database = db;
            _collectionName = collectionName;
            _collection = _database.GetCollection<T>(_collectionName);

            if (_database == null)
            {
                throw new Exception("Mongo database is null!");
            }

            if (_collection == null)
            {
                throw new Exception("Mongo collection is null!");
            }
        }

        /// <summary>
        /// Get all entities.
        /// </summary>
        /// <returns>Returns list of all entities from the Mongo as IQueryable.</returns>
        public IQueryable<T> Get()
        {
            var data = _collection.AsQueryable<T>();
            return data;
        }

        /// <summary>
        /// Get entity by id.
        /// </summary>
        /// <returns>Returns one entity from Mongo where _id == id.</returns>
        public T Get(ObjectId id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            return _collection.Find<T>(filter).FirstOrDefault<T>();
        }

        /// <summary>
        /// Add a new entity to collection.
        /// </summary>
        public void Add(T entity)
        {
            _collection.InsertOne(entity);
        }

        /// <summary>
        /// Update entity in a collection.
        /// </summary>
        public bool Update(Expression<Func<T, ObjectId>> queryExpression, ObjectId id, T entity)
        {
            var query = Builders<T>.Filter.Eq(queryExpression, id);
            return _collection.ReplaceOne(query, entity).IsAcknowledged;
        }

        /// <summary>
        /// Remove entity from a collection.
        /// </summary>
        public T Delete(Expression<Func<T, ObjectId>> queryExpression, ObjectId id)
        {
            //Get an entity we want to remove
            var entity = this.Get(id);

            //Remove the entity
            var query = Builders<T>.Filter.Eq(queryExpression, id);
            var count = _collection.DeleteOne(query).DeletedCount;

            //Return entity or null if it's not removed
            return (count > 0) ? entity : null;
        }
    }
}
