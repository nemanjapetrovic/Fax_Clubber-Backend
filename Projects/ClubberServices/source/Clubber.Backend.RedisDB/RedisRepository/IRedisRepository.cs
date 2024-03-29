﻿namespace Clubber.Backend.RedisDB.RedisRepository
{
    /// <summary>
    /// This repository will be used only with Club and Events models.
    /// In this project we only need to use Sets Redis data structure storage.
    /// We don't have need for any other of 4 Redis data structure types of storage.
    /// </summary>
    public interface IRedisRepository
    {
        string GetString(string keyModel, string keyAdditionalInfo, string keyUniqueValue);
        bool StoreString(string keyModel, string keyAdditionalInfo, string keyUniqueValue, string storeValue);
        bool RemoveString(string keyModel, string keyAdditionalInfo, string keyUniqueValue);

        string[] GetSet(string keyModel, string keyAdditionalInfo, string keyUniqueValue);
        bool StoreSet(string keyModel, string keyAdditionalInfo, string keyUniqueValue, string storeValue);
        bool RemoveSet(string keyModel, string keyAdditionalInfo, string keyUniqueValue, string storedValue);
    }
}
