using Clubber.Backend.Models.Model;
using Clubber.Backend.MongoDB.MongoServices;
using MongoDB.Bson;

namespace Clubber.Backend.Test
{
    public class MongoTest
    {
        public void AddClub()
        {
            Club b = new Club()
            {
                Name = "in da club",
                Country = "srbistan",
                Address = "Nis vrezina",
                PhoneNumber = { "018800619", "21812912" },
                Email = { "swaba@test.com", "test@test.com" },
                Image = "img test",
                Website = "url url web",
                Facebook = "url url facebook",
                Instagram = "url url instagram",
                Twitter = "url url twitter"
            };
            //  ClubService tmp = new ClubService();
            //tmp.Add(b);
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
            b._id = new ObjectId("586a4d0c9df8d523c44f284d");

            //  ClubService tmp = new ClubService();
            //tmp.Update(b);
        }

        public void DeleteClub()
        {
            //  ClubService tmp = new ClubService();
            //tmp.Delete("586a4d0c9df8d523c44f284d");
        }
    }
}
