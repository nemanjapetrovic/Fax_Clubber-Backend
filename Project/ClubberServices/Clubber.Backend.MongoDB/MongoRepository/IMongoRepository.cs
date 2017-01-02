using MongoDB.Bson;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Clubber.Backend.MongoDB.MongoRepository
{
    interface IMongoRepository<T> where T : class
    {
        IQueryable<T> Get();
        T Get(string id);
        void Add(T entity);
        void Update(Expression<Func<T, string>> queryExpression, string id, T entity);
        void Delete(Expression<Func<T, string>> queryExpression, string id);
    }
}
