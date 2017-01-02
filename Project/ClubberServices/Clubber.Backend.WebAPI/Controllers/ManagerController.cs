using Clubber.Backend.Models.Model;
using Clubber.Backend.MongoDB.MongoServices;
using MongoDB.Bson;
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
        public Manager Get(string id)
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
        public void Put(string id, [FromBody]Manager value)
        {
            value.ID = new ObjectId(id);
            _iManagerService.Update(value);
        }

        // DELETE: api/Manager/5
        public void Delete(string id)
        {
            _iManagerService.Delete(id);
        }
    }
}
