using System.Linq;
using LogModel = Clubber.Backend.Models.LogModels.Log;

namespace Clubber.Backend.Services.Logic.LogServices
{
    public interface ILogService
    {
        void Add(LogModel entity);
        IQueryable<LogModel> Get(int skip, int limit);
        IQueryable<LogModel> Get(string id);
        void Delete(string id);
    }
}
