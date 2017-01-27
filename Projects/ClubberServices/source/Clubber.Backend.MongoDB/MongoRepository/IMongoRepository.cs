using MongoDB.Bson;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Clubber.Backend.MongoDB.MongoRepository
{
    /// <summary>
    /// MongoDB is used to store all the "hard" data, typically it's all Models from Model project.
    /// </summary>
    public interface IMongoRepository<T> where T : class
    {
        IQueryable<T> Get();
        T Get(ObjectId id);
        void Add(T entity);
        bool Update(Expression<Func<T, ObjectId>> queryExpression, ObjectId id, T entity);
        bool Delete(Expression<Func<T, ObjectId>> queryExpression, ObjectId id);
    }
}
