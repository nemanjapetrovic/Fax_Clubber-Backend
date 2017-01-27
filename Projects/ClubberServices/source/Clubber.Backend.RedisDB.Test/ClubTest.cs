using Microsoft.VisualStudio.TestTools.UnitTesting;
using Clubber.Backend.RedisDB.RedisManagers;
using System.Linq;

namespace Clubber.Backend.RedisDB.Test
{
    [TestClass]
    public class ClubTest
    {
        private ClubRedisManager _redisClubManager = new ClubRedisManager("localhost:6379");

        private const string keyModel = "club";
        private const string keyAdditionalInfo = "name";
        private const string keyUniqueValue = "test club name";

        [TestMethod]
        public void Get()
        {
            // Load from RedisDB
            var objs = _redisClubManager.ClubRepository.Get(
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
            bool added = _redisClubManager.ClubRepository.Store(
                keyModel,
                keyAdditionalInfo,
                keyUniqueValue,
                hash);
            bool tmp = true;

            Assert.AreEqual(added, tmp);

            // Load from RedisDB
            var objs = _redisClubManager.ClubRepository.Get(
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
            bool removed = _redisClubManager.ClubRepository.Remove(
                keyModel,
                keyAdditionalInfo,
                keyUniqueValue,
                hash);
            bool tmp = true;

            Assert.AreEqual(removed, tmp);

            // Load from RedisDB
            var objs = _redisClubManager.ClubRepository.Get(
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
