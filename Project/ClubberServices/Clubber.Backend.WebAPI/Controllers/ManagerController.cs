using Clubber.Backend.Models.Model;
using Clubber.Backend.MongoDB.MongoServices;
using System.Collections.Generic;
using System.Web.Http;

namespace Managerber.WebAPI.Controllers
{
    public class ManagerController : ApiController
    {
        private readonly IMongoService<Manager> _iManagerService;

        public ManagerController()
        {
            _iManagerService = new ManagerService();
        }

        // GET: api/Manager
        public IEnumerable<Manager> Get()
        {
            var objs = _iManagerService.Get();
            return objs;
        }

        // GET: api/Manager/5
        public Manager Get(ObjectId id)
        {
            var obj = _iManagerService.Get(id);
            return obj;
        }

        // POST: api/Manager
        public void Post([FromBody]Manager value)
        {
            _iManagerService.Add(value);

        }

        // PUT: api/Manager/5
        public void Put(ObjectId id, [FromBody]Manager value)
        {
            value.ID = id;
            _iManagerService.Update(value);
        }

        // DELETE: api/Manager/5
        public void Delete(ObjectId id)
        {
            _iManagerService.Delete(id);
        }
    }
}
