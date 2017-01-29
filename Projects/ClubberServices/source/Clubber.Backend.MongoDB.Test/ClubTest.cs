using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using Clubber.Backend.Models.Model;
using Clubber.Backend.MongoDB.MongoManagers;
using System.Linq;

namespace Clubber.Backend.MongoDB.Test
{
    [TestClass]
    public class ClubTest
    {
        private ClubMongoManager _club = new ClubMongoManager("mongodb://localhost:27017", "clubbertest");

        [TestMethod]
        public void GetAll()
        {
            var items = _club.ClubRepository.Get();

            Assert.IsNotNull(items);
        }

        [TestMethod]
        public void Get()
        {
            // Get all
            var items = _club.ClubRepository.Get();
            Assert.IsNotNull(items);

            // Convert to list
            var listItems = items.ToList<Club>();
            Assert.IsNotNull(listItems);

            if (listItems.Count == 0)
            {
                return;
            }

            // Get only one
            ObjectId id = new ObjectId(listItems[0]._id.ToString());
            var item = _club.ClubRepository.Get(id);

            Assert.IsNotNull(item);
        }

        [TestMethod]
        public void Add()
        {
            Club club = new Club()
            {
                Name = "Nis Night",
                Country = "Serbia",
                Address = "Komren",
                PhoneNumber = { "018800619", "21812912" },
                Email = { "some@test.com", "test@test.com" },
                Website = "url url web",
                Facebook = "url url facebook",
                Instagram = "url url instagram",
                Twitter = "url url twitter"
            };
            _club.ClubRepository.Add(club);

            Assert.IsNotNull(club._id);
        }

        [TestMethod]
        public void Update()
        {
            // Get all
            var items = _club.ClubRepository.Get();
            Assert.IsNotNull(items);

            // Convert to list
            var listItems = items.ToList<Club>();
            Assert.IsNotNull(listItems);

            if (listItems.Count == 0)
            {
                return;
            }

            // Get only one
            ObjectId id = new ObjectId(listItems[0]._id.ToString());
            var itemToUpdate = _club.ClubRepository.Get(id);

            //Update
            var r = new Random();
            var ran = r.Next(100);
            Club club = new Club()
            {
                Name = "Nis Night",
                Country = "Serbia",
                Address = "Komren",
                PhoneNumber = { "018800619", "21812912" },
                Email = { "some@test.com", "test@test.com" },
                Website = "url url web " + ran.ToString(),
                Facebook = "url url facebook " + ran.ToString(),
                Instagram = "url url instagram " + ran.ToString(),
                Twitter = "url url twitter " + ran.ToString()
            };
            club._id = id;
            bool updated = _club.ClubRepository.Update(x => x._id, club._id, club);

            Assert.IsNotNull(club._id);
            Assert.IsTrue(updated);

            var updatedItem = _club.ClubRepository.Get(id);
            if (updatedItem.Facebook.Equals(itemToUpdate.Facebook))
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Delete()
        {
            // Get all
            var items = _club.ClubRepository.Get();
            Assert.IsNotNull(items);

            // Convert to list
            var listItems = items.ToList<Club>();
            Assert.IsNotNull(listItems);

            if (listItems.Count == 0)
            {
                return;
            }

            // Get only one
            ObjectId id = new ObjectId(listItems[0]._id.ToString());
            var deletedClub = _club.ClubRepository.Delete(item => item._id, id);

            Assert.IsTrue(deletedClub);

            var isRemoved = _club.ClubRepository.Get(id);
            Assert.IsNull(isRemoved);
        }
    }
}
