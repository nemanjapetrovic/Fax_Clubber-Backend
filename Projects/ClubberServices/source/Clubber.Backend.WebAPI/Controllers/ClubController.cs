using Clubber.Backend.Models.Model;
using Clubber.Backend.Services.Logic.Services;
using Clubber.Backend.WebAPI.Helpers;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;

namespace Clubber.WebAPI.Controllers
{
    public class ClubController : ApiController
    {
        private readonly IService<Club> _iClubService;

        public ClubController()
        {
            //connection strings
            string mongoConStr = ConfigurationManager.ConnectionStrings[Constants.MongoDB.MongoDBConectionString].ConnectionString;
            string redisConStr = ConfigurationManager.ConnectionStrings[Constants.RedisDB.RedisDBConectionString].ConnectionString;
            //mongodb name
            string mongoDbName = ConfigurationManager.AppSettings[Constants.MongoDB.MongoDBDatabaseName];

            _iClubService = new ClubService(mongoConStr, mongoDbName, redisConStr);
        }

        // GET: api/Club/5
        public IEnumerable<Club> Get(string id)
        {
            var objs = _iClubService.Get(id);
            return objs;
        }

        // POST: api/Club
        public void Post([FromBody]Club value)
        {
            if (!ModelState.IsValid)
            {
                return;
            }

            _iClubService.Add(value);
        }

        // PUT: api/Club/5
        public void Put(string id, [FromBody]Club value)
        {
            value._id = id;
            _iClubService.Update(value);
        }

        // DELETE: api/Club/5
        public void Delete(string id)
        {
            _iClubService.Delete(id);
        }
    }
}
