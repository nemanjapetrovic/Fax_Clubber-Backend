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
        IQueryable<T> Get(int skip, int limit);
        T Get(string id);
        void Add(T entity);
        bool Update(Expression<Func<T, string>> queryExpression, string id, T entity);
        T Delete(Expression<Func<T, string>> queryExpression, string id);
    }
}
