using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Clubber.Backend.Models.DomainModels;
using Clubber.Backend.MongoDB.MongoManagers;
using System.Linq;

namespace Clubber.Backend.MongoDB.Test
{
    [TestClass]
    public class UserTest
    {
        private UserMongoManager _user = new UserMongoManager("mongodb://localhost:27017", "clubbertest");

        [TestMethod]
        public void GetAll()
        {
            var items = _user.UserRepository.Get();

            Assert.IsNotNull(items);
        }

        [TestMethod]
        public void Get()
        {
            // Get all
            var items = _user.UserRepository.Get();
            Assert.IsNotNull(items);

            // Convert to list
            var listItems = items.ToList<User>();
            Assert.IsNotNull(listItems);

            if (listItems.Count == 0)
            {
                return;
            }

            // Get only one
            var id = listItems[0]._id.ToString();
            var item = _user.UserRepository.Get(id);

            Assert.IsNotNull(item);
        }

        [TestMethod]
        public void Add()
        {
            User someUser = new User()
            {
                FirstName = "TestName",
                LastName = "TestLastName",
                Username = "TesticNeki"
            };
            _user.UserRepository.Add(someUser);

            Assert.IsNotNull(someUser._id);
        }

        [TestMethod]
        public void Update()
        {
            // Get all
            var items = _user.UserRepository.Get();
            Assert.IsNotNull(items);

            // Convert to list
            var listItems = items.ToList<User>();
            Assert.IsNotNull(listItems);

            if (listItems.Count == 0)
            {
                return;
            }

            // Get only one
            var id = listItems[0]._id.ToString();
            var itemToUpdate = _user.UserRepository.Get(id);

            //Update
            var r = new Random();
            var ran = r.Next(100);
            User someUser = new User()
            {
                FirstName = "TestName",
                LastName = "TestLastName" + ran.ToString(),
                Username = "TesticNeki"
            };
            someUser._id = id;
            bool added = _user.UserRepository.Update(x => x._id, someUser._id, someUser);
            bool test = true;

            Assert.IsNotNull(someUser._id);
            Assert.AreEqual(added, test);

            var updatedItem = _user.UserRepository.Get(id);
            if (updatedItem.LastName.Equals(itemToUpdate.LastName))
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Delete()
        {
            // Get all
            var items = _user.UserRepository.Get();
            Assert.IsNotNull(items);

            // Convert to list
            var listItems = items.ToList<User>();
            Assert.IsNotNull(listItems);

            if (listItems.Count == 0)
            {
                return;
            }

            // Get only one
            var id = listItems[0]._id.ToString();
            var deletedClub = _user.UserRepository.Delete(item => item._id, id);

            Assert.IsNotNull(deletedClub);

            var isRemoved = _user.UserRepository.Get(id);
            Assert.IsNull(isRemoved);
        }
    }
}