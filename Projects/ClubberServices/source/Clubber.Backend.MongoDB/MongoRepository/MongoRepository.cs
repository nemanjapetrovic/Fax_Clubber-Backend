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
        /// <param name="db">Mongo database.</param>
        /// <param name="collectionName">Name of the collection in the database.</param>
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
        /// <param name="id">Id of the entity stored in database.</param>
        /// <returns>Returns one entity from Mongo where _id == id.</returns>
        public T Get(ObjectId id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            return _collection.Find<T>(filter).FirstOrDefault<T>();
        }

        /// <summary>
        /// Add a new entity to collection.
        /// You will need to validate if the entity has _id != null,
        /// because this function will not do any validation.
        /// </summary>
        /// <param name="entity">An entity that will be stored in database.</param>
        public void Add(T entity)
        {
            _collection.InsertOne(entity);
        }

        /// <summary>
        /// Update entity in a collection.
        /// </summary>
        /// <param name="queryExpression">Lambda expression. item=>item.id</param>
        /// <param name="id">Id of the entity stored in database.</param>
        /// <param name="entity">Entity with updated values.</param>
        /// <returns>True if the entity is updated.</returns>
        public bool Update(Expression<Func<T, ObjectId>> queryExpression, ObjectId id, T entity)
        {
            var query = Builders<T>.Filter.Eq(queryExpression, id);
            var isUpdated = _collection.ReplaceOne(query, entity).ModifiedCount;

            return (isUpdated > 0);
        }

        /// <summary>
        /// Remove entity from a collection.
        /// </summary>
        /// <param name="queryExpression">Lambda expression. item=>item.id</param>
        /// <param name="id">Id of the entity stored in database.</param>
        /// <returns>True if the entity is removed from database.</returns>
        public bool Delete(Expression<Func<T, ObjectId>> queryExpression, ObjectId id)
        {
            var query = Builders<T>.Filter.Eq(queryExpression, id);
            var isDeleted = _collection.DeleteOne(query).DeletedCount;

            return (isDeleted > 0);
        }
    }
}
