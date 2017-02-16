using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Clubber.Backend.Models.DomainModels;
using Clubber.Backend.MongoDB.MongoManagers;
using System.Linq;

namespace Clubber.Backend.MongoDB.Test
{
    [TestClass]
    public class ManagerTest
    {
        private ManagerMongoManager _manager = new ManagerMongoManager("mongodb://localhost:27017", "clubbertest");

        [TestMethod]
        public void GetAll()
        {
            var items = _manager.ManagerRepository.Get();

            Assert.IsNotNull(items);
        }

        [TestMethod]
        public void Get()
        {
            // Get all
            var items = _manager.ManagerRepository.Get();
            Assert.IsNotNull(items);

            // Convert to list
            var listItems = items.ToList<Manager>();
            Assert.IsNotNull(listItems);

            if (listItems.Count == 0)
            {
                return;
            }

            // Get only one
            var id = listItems[0]._id.ToString();
            var item = _manager.ManagerRepository.Get(id);

            Assert.IsNotNull(item);
        }

        [TestMethod]
        public void Add()
        {
            Manager manager = new Manager()
            {
                FirstName = "TestName",
                LastName = "TestLastName",
                City = "Neki",
                Country = "neka",
                Email = "nemanja@test.com",
                Password = "Test12345@",
                PostalCode = "18220"
            };
            _manager.ManagerRepository.Add(manager);

            Assert.IsNotNull(manager._id);
        }

        [TestMethod]
        public void Update()
        {
            // Get all
            var items = _manager.ManagerRepository.Get();
            Assert.IsNotNull(items);

            // Convert to list
            var listItems = items.ToList<Manager>();
            Assert.IsNotNull(listItems);

            if (listItems.Count == 0)
            {
                return;
            }

            // Get only one
            var id = listItems[0]._id.ToString();
            var itemToUpdate = _manager.ManagerRepository.Get(id);

            //Update
            var r = new Random();
            var ran = r.Next(100);
            Manager manager = new Manager()
            {
                FirstName = "TestName",
                LastName = "TestLastName" + ran.ToString(),
                City = "Neki",
                Country = "neka",
                Email = "nemanja@test.com",
                Password = "Test12345@",
                PostalCode = "18220"
            };
            manager._id = id;
            bool added = _manager.ManagerRepository.Update(x => x._id, manager._id, manager);
            bool test = true;

            Assert.IsNotNull(manager._id);
            Assert.AreEqual(added, test);

            var updatedItem = _manager.ManagerRepository.Get(id);
            if (updatedItem.LastName.Equals(itemToUpdate.LastName))
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Delete()
        {
            // Get all
            var items = _manager.ManagerRepository.Get();
            Assert.IsNotNull(items);

            // Convert to list
            var listItems = items.ToList<Manager>();
            Assert.IsNotNull(listItems);

            if (listItems.Count == 0)
            {
                return;
            }

            // Get only one
            var id = listItems[0]._id.ToString();
            var deletedClub = _manager.ManagerRepository.Delete(item => item._id, id);

            Assert.IsNotNull(deletedClub);

            var isRemoved = _manager.ManagerRepository.Get(id);
            Assert.IsNull(isRemoved);
        }
    }
}