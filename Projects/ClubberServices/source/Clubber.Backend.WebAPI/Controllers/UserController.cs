using Clubber.Backend.Models.Model;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Web.Http;
using Clubber.Backend.WebAPI.Helpers;
using System.Configuration;
using Clubber.Backend.Services.Logic.Services;

namespace Userber.WebAPI.Controllers
{
    public class UserController : ApiController
    {
        private readonly IService<User> _iUserService;

        public UserController()
        {
            //connection strings
            string mongoConStr = ConfigurationManager.ConnectionStrings[Constants.MongoDB.MongoDBConectionString].ConnectionString;
            string redisConStr = ConfigurationManager.ConnectionStrings[Constants.RedisDB.RedisDBConectionString].ConnectionString;
            //mongodb name
            string mongoDbName = ConfigurationManager.AppSettings[Constants.MongoDB.MongoDBDatabaseName];

            _iUserService = new UserService(mongoConStr, mongoDbName, redisConStr);
        }

        // GET: api/User/5
        public IEnumerable<User> Get(string id)
        {
            var obj = _iUserService.Get(id);
            return obj;
        }

        // POST: api/User
        public void Post([FromBody]User value)
        {
            if (!ModelState.IsValid)
            {
                return;
            }

            _iUserService.Add(value);
        }

        // PUT: api/User/5
        public void Put(string id, [FromBody]User value)
        {
            value._id = new ObjectId(id);
            _iUserService.Update(value);
        }

        // DELETE: api/User/5
        public void Delete(string id)
        {
            _iUserService.Delete(id);
        }
    }
}
