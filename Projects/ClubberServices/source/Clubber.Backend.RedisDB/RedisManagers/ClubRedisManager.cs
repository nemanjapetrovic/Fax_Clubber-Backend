﻿using Clubber.Backend.RedisDB.RedisRepository;
using Clubber.Common.Exceptions.Exceptions;
using System;

namespace Clubber.Backend.RedisDB.RedisManagers
{
    public class ClubRedisManager
    {
        private string _connectionString;

        protected IRedisRepository _clubRepo = null;
        public IRedisRepository ClubRepository
        {
            get
            {
                if (_clubRepo == null)
                {
                    _clubRepo = new RedisRepository.RedisRepository(_connectionString);
                }
                return _clubRepo;
            }
        }

        public ClubRedisManager(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InternalServerErrorException("Redis connection string is empty!");
            }

            _connectionString = connectionString;
        }
    }
}
