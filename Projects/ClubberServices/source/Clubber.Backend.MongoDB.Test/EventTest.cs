using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using Clubber.Backend.Models.Model;
using Clubber.Backend.MongoDB.MongoManagers;
using System.Linq;

namespace Clubber.Backend.MongoDB.Test
{
    [TestClass]
    public class EventTest
    {
        private EventMongoManager _event = new EventMongoManager("mongodb://localhost:27017", "clubbertest");

        [TestMethod]
        public void GetAll()
        {
            var items = _event.EventRepository.Get();

            Assert.IsNotNull(items);
        }

        [TestMethod]
        public void Get()
        {
            // Get all
            var items = _event.EventRepository.Get();
            Assert.IsNotNull(items);

            // Convert to list
            var listItems = items.ToList<Event>();
            Assert.IsNotNull(listItems);

            if (listItems.Count == 0)
            {
                return;
            }

            // Get only one
            ObjectId id = new ObjectId(listItems[0]._id.ToString());
            var item = _event.EventRepository.Get(id);

            Assert.IsNotNull(item);
        }

        [TestMethod]
        public void Add()
        {
            Event eventt = new Event()
            {
                Name = "In the club event",
                Description = "Playhouse",
                EndDateTime = new DateTime(),
                StartDateTime = new DateTime(),
            };
            _event.EventRepository.Add(eventt);

            Assert.IsNotNull(eventt._id);
        }

        [TestMethod]
        public void Update()
        {
            // Get all
            var items = _event.EventRepository.Get();
            Assert.IsNotNull(items);

            // Convert to list
            var listItems = items.ToList<Event>();
            Assert.IsNotNull(listItems);

            if (listItems.Count == 0)
            {
                return;
            }

            // Get only one
            ObjectId id = new ObjectId(listItems[0]._id.ToString());
            var itemToUpdate = _event.EventRepository.Get(id);

            //Update
            var r = new Random();
            var ran = r.Next(100);
            Event eventt = new Event()
            {
                Name = "In the club event",
                Description = "Playhouse" + ran.ToString(),
                EndDateTime = new DateTime(),
                StartDateTime = new DateTime(),
            };
            eventt._id = id;
            bool added = _event.EventRepository.Update(x => x._id, eventt._id, eventt);
            bool test = true;

            Assert.IsNotNull(eventt._id);
            Assert.AreEqual(added, test);

            var updatedItem = _event.EventRepository.Get(id);
            if (updatedItem.Description.Equals(itemToUpdate.Description))
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Delete()
        {
            // Get all
            var items = _event.EventRepository.Get();
            Assert.IsNotNull(items);

            // Convert to list
            var listItems = items.ToList<Event>();
            Assert.IsNotNull(listItems);

            if (listItems.Count == 0)
            {
                return;
            }

            // Get only one
            ObjectId id = new ObjectId(listItems[0]._id.ToString());
            var deletedClub = _event.EventRepository.Delete(item => item._id, id);

            Assert.IsNotNull(deletedClub);

            var isRemoved = _event.EventRepository.Get(id);
            Assert.IsNull(isRemoved);
        }
    }
}