using Clubber.Backend.Models.Model;
using System.Collections.Generic;
using System.Web.Http;
using Clubber.Backend.WebAPI.Helpers;
using System.Configuration;
using Clubber.Backend.Services.Logic.Services;
using System;
using System.Linq;

namespace Userber.WebAPI.Controllers
{
    public class UserController : ApiController
    {
        private readonly IService<User> _iUserService;

        public UserController()
        {
            // Connection strings
            string mongoConStr = ConfigurationManager.ConnectionStrings[Constants.MongoDB.MongoDBConectionString].ConnectionString;
            string redisConStr = ConfigurationManager.ConnectionStrings[Constants.RedisDB.RedisDBConectionString].ConnectionString;
            // MongoDB name
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
        public IHttpActionResult Post([FromBody]User value)
        {
            // Validation
            if (!ModelState.IsValid)
            {
                string messages = string.Join(Environment.NewLine, ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                return BadRequest(messages);
            }

            // Call
            _iUserService.Add(value);

            // Ret
            return Ok();
        }

        // PUT: api/User/5
        public IHttpActionResult Put(string id, [FromBody]User value)
        {
            // Validation
            if (!ModelState.IsValid)
            {
                string messages = string.Join(Environment.NewLine, ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                return BadRequest(messages);
            }

            // Call
            value._id = id;
            _iUserService.Update(value);

            // Ret
            return Ok();
        }

        // DELETE: api/User/5
        public IHttpActionResult Delete(string id)
        {
            // Validation
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Id is empty");
            }

            // Call
            _iUserService.Delete(id);

            // Ret
            return Ok();
        }
    }
}
