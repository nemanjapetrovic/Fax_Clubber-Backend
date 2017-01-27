using Microsoft.VisualStudio.TestTools.UnitTesting;
using Clubber.Backend.RedisDB.RedisManagers;
using System.Linq;

namespace Clubber.Backend.RedisDB.Test
{
    [TestClass]
    public class EventTest
    {
        private EventRedisManager _redisEventManager = new EventRedisManager("localhost:6379");

        private const string keyModel = "event";
        private const string keyAdditionalInfo = "name";
        private const string keyUniqueValue = "test event name";

        [TestMethod]
        public void Get()
        {
            // Load from RedisDB
            var objs = _redisEventManager.EventRepository.Get(
                keyModel,
                keyAdditionalInfo,
                keyUniqueValue);

            Assert.IsNotNull(objs);
        }

        [TestMethod]
        public void Store()
        {
            string hash = "hesh-1234-test-hash";
            // Add _id to RedisDB Sets
            bool added = _redisEventManager.EventRepository.Store(
                keyModel,
                keyAdditionalInfo,
                keyUniqueValue,
                hash);
            bool tmp = true;

            Assert.AreEqual(added, tmp);

            // Load from RedisDB
            var objs = _redisEventManager.EventRepository.Get(
                keyModel,
                keyAdditionalInfo,
                keyUniqueValue);

            Assert.IsNotNull(objs);

            // Check if the hash is stored
            bool contains = objs.Contains<string>(hash);
            if (!contains)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Remove()
        {
            string hash = "hesh-1234-test-hash";

            //Remove from RedisDB
            bool removed = _redisEventManager.EventRepository.Remove(
                keyModel,
                keyAdditionalInfo,
                keyUniqueValue,
                hash);
            bool tmp = true;

            Assert.AreEqual(removed, tmp);

            // Load from RedisDB
            var objs = _redisEventManager.EventRepository.Get(
                keyModel,
                keyAdditionalInfo,
                keyUniqueValue);

            Assert.IsNotNull(objs);

            // Check if the hash is stored
            bool contains = objs.Contains<string>(hash);
            if (contains)
            {
                Assert.Fail();
            }
        }
    }
}
