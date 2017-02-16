using Clubber.Backend.Models.DomainModels;
using System.Collections.Generic;
using System.Web.Http;
using Clubber.Backend.WebAPI.Helpers;
using System.Configuration;
using Clubber.Backend.Services.Logic.Services;
using System;
using System.Linq;
using Clubber.Common.Exceptions.Exceptions;

namespace Managerber.WebAPI.Controllers
{
    public class ManagerController : ApiController
    {
        private readonly IService<Manager> _iManagerService;

        public ManagerController()
        {
            // Connection strings
            string mongoConStr = ConfigurationManager.ConnectionStrings[Constants.MongoDB.MongoDBConectionString].ConnectionString;
            string redisConStr = ConfigurationManager.ConnectionStrings[Constants.RedisDB.RedisDBConectionString].ConnectionString;
            // MongoDB name
            string mongoDbName = ConfigurationManager.AppSettings[Constants.MongoDB.MongoDBDatabaseName];

            _iManagerService = new ManagerService(mongoConStr, mongoDbName, redisConStr);
        }

        // GET: api/Manager/5
        public IEnumerable<Manager> Get(string id)
        {
            try
            {
                var obj = _iManagerService.Get(id);
                return obj;
            }
            catch (InternalServerErrorException ex)
            {
                return new List<Manager>();
            }
            catch (Exception ex)
            {
                return new List<Manager>();
            }
        }

        // POST: api/Manager
        public IHttpActionResult Post([FromBody]Manager value)
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
                _iManagerService.Add(value);

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

        // PUT: api/Manager/5
        public IHttpActionResult Put(string id, [FromBody]Manager value)
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
                _iManagerService.Update(value);

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

        // DELETE: api/Manager/5
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
                _iManagerService.Delete(id);

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
