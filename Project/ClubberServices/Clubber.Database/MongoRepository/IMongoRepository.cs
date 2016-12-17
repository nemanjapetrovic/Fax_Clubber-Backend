using System;
using System.Linq;
using System.Linq.Expressions;

namespace Clubber.Database.MongoRepository
{
    interface IMongoRepository<T> where T : class
    {
        IQueryable<T> Get();
        T Get(int id);
        void Add(T entity);
        void Update(Expression<Func<T, int>> queryExpression, int id, T entity);
        void Delete(Expression<Func<T, int>> queryExpression, int id);
    }
}
