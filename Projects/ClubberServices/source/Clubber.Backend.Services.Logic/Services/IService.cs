using System.Linq;

namespace Clubber.Backend.Services.Logic.Services
{
    public interface IService<T> where T : class
    {
        IQueryable<T> Get(string id);
        void Add(T entity);
        void Update(T entity);
        void Delete(string id);
    }
}
