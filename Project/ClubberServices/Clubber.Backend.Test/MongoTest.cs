using Clubber.Backend.Models.Model;
using Clubber.Backend.MongoDB.MongoServices;
using MongoDB.Bson;

namespace Clubber.Backend.Test
{
    public class MongoTest
    {
        public static string RedisDBConectionString = "localhost:6379";
        public static string MongoDBConectionString = "mongodb://localhost:27017";
        public static string MongoDBDatabaseName = "clubbertest";

        public void AddClub()
        {
            Club b = new Club()
            {
                Name = "Nis Nocu",
                Country = "srbistan",
                Address = "KOMREN",
                PhoneNumber = { "018800619", "21812912" },
                Email = { "swaba@test.com", "test@test.com" },
                Image = "img test",
                Website = "url url web",
                Facebook = "url url facebook",
                Instagram = "url url instagram",
                Twitter = "url url twitter"
            };
            ClubService tmp = new ClubService(MongoDBConectionString, MongoDBDatabaseName, RedisDBConectionString);
            tmp.Add(b);
        }

        public void UpdateClub()
        {
            Club b = new Club()
            {
                Name = "in the club",
                Country = "cukurevac",
                Address = "donja vrezina",
                PhoneNumber = { "018800619", "21812912" },
                Email = { "swaba@test.com", "test@test.com" },
                Image = "img test",
                Website = "url url web",
                Facebook = "url url facebook",
                Instagram = "url url instagram",
                Twitter = "url url twitter"
            };
            b._id = new ObjectId("586d2e029df8d51da04f8d70");

            ClubService tmp = new ClubService(MongoDBConectionString, MongoDBDatabaseName, RedisDBConectionString);
            tmp.Update(b);
        }

        public void DeleteClub()
        {
            ClubService tmp = new ClubService(MongoDBConectionString, MongoDBDatabaseName, RedisDBConectionString);
            tmp.Delete("587e7dd89df8d507d0411d98");
        }


        public void AddEvent()
        {
            Event b = new Event()
            {
                Address = "test adresa",
                EndDateTime = new System.DateTime(),
                StartDateTime = new System.DateTime(),
                Name = "intheclubevent"
            };
            EventService tmp = new EventService(MongoDBConectionString, MongoDBDatabaseName, RedisDBConectionString);
            tmp.Add(b);
        }

        public void UpdateEvent()
        {
            Event b = new Event()
            {
                Address = "mosha adresa",
                EndDateTime = new System.DateTime(),
                StartDateTime = new System.DateTime(),
                Name = "intheclubevent",
                Description = "testic"
            };

            b._id = new ObjectId("586fbd7b9df8d52744bc597b");

            EventService tmp = new EventService(MongoDBConectionString, MongoDBDatabaseName, RedisDBConectionString);
            tmp.Update(b);
        }

        public void DeleteEvent()
        {
            EventService tmp = new EventService(MongoDBConectionString, MongoDBDatabaseName, RedisDBConectionString);
            tmp.Delete("586fbd7b9df8d52744bc597b");
        }
    }
}
