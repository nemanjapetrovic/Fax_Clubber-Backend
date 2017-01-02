using Clubber.Backend.Models.Model;
using Clubber.Backend.MongoDB.MongoServices;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Web.Http;

namespace Userber.WebAPI.Controllers
{
    public class UserController : ApiController
    {
        private readonly IMongoService<User> _iUserService;

        public UserController()
        {
            _iUserService = new UserService();
        }

        // GET: api/User
        public IEnumerable<User> Get()
        {
            var objs = _iUserService.Get();
            return objs;
        }

        // GET: api/User/5
        public User Get(string id)
        {
            var obj = _iUserService.Get(id);
            return obj;
        }

        // POST: api/User
        public void Post([FromBody]User value)
        {
            _iUserService.Add(value);

        }

        // PUT: api/User/5
        public void Put(string id, [FromBody]User value)
        {
            value.ID = new ObjectId(id);
            _iUserService.Update(value);
        }

        // DELETE: api/User/5
        public void Delete(string id)
        {
            _iUserService.Delete(id);
        }
    }
}
