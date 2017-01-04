using System.Linq;
using Clubber.Backend.Models.Model;
using Clubber.Backend.MongoDB.MongoManagers;
using MongoDB.Bson;

namespace Clubber.Backend.MongoDB.MongoServices
{
    public class ClubService : IService<Club>
    {
        private readonly ClubMongoManager _mongoClubManager;

        public ClubService(string mongoConStr,
                           string mongoDbName,
                           string redisConStr)
        {
            _mongoClubManager = new ClubMongoManager(mongoConStr, mongoDbName);
        }

        public void Add(Club entity)
        {
            _mongoClubManager.ClubRepository.Add(entity);
        }

        public IQueryable<Club> Get()
        {
            return _mongoClubManager.ClubRepository.Get();
        }

        public Club Get(string id)
        {
            return _mongoClubManager.ClubRepository.Get(new ObjectId(id));
        }

        public void Update(Club entity)
        {
            _mongoClubManager.ClubRepository.Update(item => item._id, entity._id, entity);
        }

        public void Delete(string id)
        {
            _mongoClubManager.ClubRepository.Delete(item => item._id, new ObjectId(id));
        }
    }
}
