using Clubber.Backend.Models.DomainModels;
using Clubber.Backend.Services.Logic.Services;
using Clubber.Backend.WebAPI.Helpers;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;
using System;
using Clubber.Common.Exceptions.Exceptions;
using System.Text.RegularExpressions;

namespace Clubber.WebAPI.Controllers
{
    public class ClubController : ApiController
    {
        private readonly IService<Club> _iClubService;

        public ClubController()
        {
            // Connection strings
            string mongoConStr = ConfigurationManager.ConnectionStrings[Constants.MongoDB.MongoDBConectionString].ConnectionString;
            string redisConStr = ConfigurationManager.ConnectionStrings[Constants.RedisDB.RedisDBConectionString].ConnectionString;
            // MongoDB name
            string mongoDbName = ConfigurationManager.AppSettings[Constants.MongoDB.MongoDBDatabaseName];

            _iClubService = new ClubService(mongoConStr, mongoDbName, redisConStr);
        }

        // GET: api/Club/5
        public IEnumerable<Club> Get(string name)
        {
            try
            {
                var objs = _iClubService.Get(Regex.Replace(name, @"\s+", "").ToLower());
                return objs;
            }
            catch (InternalServerErrorException ex)
            {
                return new List<Club>();
            }
            catch (Exception ex)
            {
                return new List<Club>();
            }
        }

        // POST: api/Club
        public IHttpActionResult Post([FromBody]Club value)
        {
            try
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
                _iClubService.Add(value);

                // Ret
                return Ok();
            }
            catch (InternalServerErrorException ex)
            {
                return InternalServerError();
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        // PUT: api/Club/5
        public IHttpActionResult Put(string id, [FromBody]Club value)
        {
            try
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
                _iClubService.Update(value);

                // Ret
                return Ok();
            }
            catch (InternalServerErrorException ex)
            {
                return InternalServerError();
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        // DELETE: api/Club/5
        public IHttpActionResult Delete(string id)
        {
            try
            {
                // Validation
                if (string.IsNullOrEmpty(id))
                {
                    return BadRequest("Id is empty");
                }

                // Call
                _iClubService.Delete(id);

                // Ret
                return Ok();
            }
            catch (InternalServerErrorException ex)
            {
                return InternalServerError();
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }
    }
}
