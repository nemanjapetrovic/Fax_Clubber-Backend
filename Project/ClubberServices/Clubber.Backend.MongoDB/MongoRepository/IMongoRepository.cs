using MongoDB.Bson;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Clubber.Backend.MongoDB.MongoRepository
{
    public interface IMongoRepository<T> where T : class
    {
        IQueryable<T> Get();
        T Get(ObjectId id);
        void Add(T entity);
        void Update(Expression<Func<T, ObjectId>> queryExpression, ObjectId id, T entity);
        void Delete(Expression<Func<T, ObjectId>> queryExpression, ObjectId id);
    }
}
