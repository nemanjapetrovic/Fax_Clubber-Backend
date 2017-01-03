namespace Clubber.Backend.RedisDB.RedisRepository
{
    /// <summary>
    /// This repository will be used only with Club and Events models.
    /// In this project we only need to use String Redis data structure storage.
    /// We don't have need for any other of 4 Redis data structure types of storage.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRedisRepository
    {
        string Get(string keyModel, string keyAdditionalInfo, string keyUniqueValue);
        bool Store(string keyModel, string keyAdditionalInfo, string keyUniqueValue, string storeValue);
        bool Update(string keyModel, string keyAdditionalInfo, string keyUniqueValue, string storeValue);
    }
}
