﻿using Clubber.Backend.Models.Model;
using Clubber.Backend.MongoDB.MongoServices;
using System.Collections.Generic;
using System.Web.Http;

namespace Clubber.WebAPI.Controllers
{
    public class ClubController : ApiController
    {
        private readonly IMongoService<Club> _iClubService;

        public ClubController()
        {
            _iClubService = new ClubService();
        }

        // GET: api/Club
        public IEnumerable<Club> Get()
        {
            var objs = _iClubService.Get();
            return objs;
        }

        // GET: api/Club/5
        public Club Get(ObjectId id)
        {
            var obj = _iClubService.Get(id);
            return obj;
        }

        // POST: api/Club
        public void Post([FromBody]Club value)
        {
            _iClubService.Add(value);

        }

        // PUT: api/Club/5
        public void Put(ObjectId id, [FromBody]Club value)
        {
            value.ID = id;
            _iClubService.Update(value);
        }

        // DELETE: api/Club/5
        public void Delete(ObjectId id)
        {
            _iClubService.Delete(id);
        }
    }
}
