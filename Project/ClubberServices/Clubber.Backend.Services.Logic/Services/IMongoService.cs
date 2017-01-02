using MongoDB.Bson;
using System.Linq;

namespace Clubber.Backend.MongoDB.MongoServices
{
    public interface IMongoService<T> where T : class
    {
        IQueryable<T> Get();
        T Get(string id);
        void Add(T entity);
        void Update(T entity);
        void Delete(string id);
    }
}
