using System.Collections.Generic;
using System.Linq;

namespace Clubber.Backend.MongoDB.MongoServices
{
    public interface IService<T> where T : class
    {
        IQueryable<T> Get();
        IQueryable<T> Get(string id);
        void Add(T entity);
        void Update(T entity);
        void Delete(string id);
    }
}
