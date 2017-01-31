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
        private const string keyAdditionalId = "id";
        private const string keyUniqueValue = "test club name";

        [TestMethod]
        public void GetSet()
        {
            // Load from RedisDB
            var objs = _redisClubManager.ClubRepository.GetSet(
                keyModel,
                keyAdditionalInfo,
                keyUniqueValue);

            Assert.IsNotNull(objs);
        }

        [TestMethod]
        public void StoreSet()
        {
            string hash = "hesh-1234-test-hash";
            // Add _id to RedisDB Sets
            bool added = _redisClubManager.ClubRepository.StoreSet(
                keyModel,
                keyAdditionalInfo,
                keyUniqueValue,
                hash);

            Assert.IsTrue(added);

            // Load from RedisDB
            var objs = _redisClubManager.ClubRepository.GetSet(
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
        public void RemoveSet()
        {
            string hash = "hesh-1234-test-hash";

            //Remove from RedisDB
            bool removed = _redisClubManager.ClubRepository.RemoveSet(
                keyModel,
                keyAdditionalInfo,
                keyUniqueValue,
                hash);

            Assert.IsTrue(removed);

            // Load from RedisDB
            var objs = _redisClubManager.ClubRepository.GetSet(
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

        [TestMethod]
        public void GetString()
        {
            string hash = "hesh-1234-test-hash";
            string data = "SOME";

            var objs = _redisClubManager.ClubRepository.GetString(
                keyModel,
                keyAdditionalId,
                hash);
            if (string.IsNullOrEmpty(objs))
            {
                Assert.Fail();
            }

            Assert.IsTrue(objs.Contains(data));
        }

        [TestMethod]
        public void StoreString()
        {
            string hash = "hesh-1234-test-hash";
            string data = "SOME STRING DATA";

            bool added = _redisClubManager.ClubRepository.StoreString(
                keyModel,
                keyAdditionalId,
                hash,
                data);

            Assert.IsTrue(added);

            // Load from RedisDB
            var objs = _redisClubManager.ClubRepository.GetString(
                keyModel,
                keyAdditionalId,
                hash);

            Assert.IsNotNull(objs);

            // Check if the hash is stored
            bool contains = objs.Contains(data);
            if (!contains)
            {
                Assert.Fail();
            }

            data += "1";

            added = _redisClubManager.ClubRepository.StoreString(
                keyModel,
                keyAdditionalId,
                hash,
                data);

            Assert.IsTrue(added);

            // Load from RedisDB
            objs = _redisClubManager.ClubRepository.GetString(
                keyModel,
                keyAdditionalId,
                hash);

            Assert.IsNotNull(objs);

            // Check if the hash is stored
            contains = objs.Contains("1");
            if (!contains)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void RemoveString()
        {
            string hash = "hesh-1234-test-hash";
            string data = "SOME";

            //Remove from RedisDB
            bool removed = _redisClubManager.ClubRepository.RemoveString(
                keyModel,
                keyAdditionalId,
                hash);

            Assert.IsTrue(removed);

            // Load from RedisDB
            var objs = _redisClubManager.ClubRepository.GetString(
                keyModel,
                keyAdditionalInfo,
                hash);

            Assert.IsNull(objs);
        }

    }
}
