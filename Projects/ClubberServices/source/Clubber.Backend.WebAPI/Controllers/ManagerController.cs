using Clubber.Backend.Models.Model;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Web.Http;
using Clubber.Backend.WebAPI.Helpers;
using System.Configuration;
using Clubber.Backend.Services.Logic.Services;

namespace Managerber.WebAPI.Controllers
{
    public class ManagerController : ApiController
    {
        private readonly IService<Manager> _iManagerService;

        public ManagerController()
        {
            //connection strings
            string mongoConStr = ConfigurationManager.ConnectionStrings[Constants.MongoDB.MongoDBConectionString].ConnectionString;
            string redisConStr = ConfigurationManager.ConnectionStrings[Constants.RedisDB.RedisDBConectionString].ConnectionString;
            //mongodb name
            string mongoDbName = ConfigurationManager.AppSettings[Constants.MongoDB.MongoDBDatabaseName];

            _iManagerService = new ManagerService(mongoConStr, mongoDbName, redisConStr);
        }

        // GET: api/Manager/5
        public IEnumerable<Manager> Get(string id)
        {
            var obj = _iManagerService.Get(id);
            return obj;
        }

        // POST: api/Manager
        public void Post([FromBody]Manager value)
        {
            if (!ModelState.IsValid)
            {
                return;
            }

            _iManagerService.Add(value);
        }

        // PUT: api/Manager/5
        public void Put(string id, [FromBody]Manager value)
        {
            value._id = new ObjectId(id);
            _iManagerService.Update(value);
        }

        // DELETE: api/Manager/5
        public void Delete(string id)
        {
            _iManagerService.Delete(id);
        }
    }
}
