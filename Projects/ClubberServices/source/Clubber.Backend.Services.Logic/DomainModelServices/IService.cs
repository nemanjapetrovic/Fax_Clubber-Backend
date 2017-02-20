using System.Linq;

namespace Clubber.Backend.Services.Logic.DomainModelServices
{
    public interface IService<T> where T : class
    {
        IQueryable<T> Get(string value);
        void Add(T entity);
        void Update(T entity);
        void Delete(string id);
    }
}
