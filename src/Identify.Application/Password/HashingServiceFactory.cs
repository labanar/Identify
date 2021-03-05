using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identify.Application.Password
{
    public class HashingServiceFactory : IHashingServiceFactory
    {
        public Task<IHashingService> CreateHashingService(HashingAlgorithm algorithm)
        {
            switch (algorithm)
            {
                case HashingAlgorithm.PBKDF2_HMACSHA1_1000:
                    return Task.FromResult<IHashingService>(new Pbkdf2HashingService_HMACSHA1_1000());
                case HashingAlgorithm.PBKDF2_HMACSHA256_1000:
                    return Task.FromResult<IHashingService>(new Pbkdf2HashingService_HMACSHA256_1000());
                case HashingAlgorithm.PBKDF2_HMACSHA512_1000:
                    return Task.FromResult<IHashingService>(new Pbkdf2HashingService_HMACSHA512_1000());
                default: throw new ApplicationException($"Error creating hashing service with algorithm [{algorithm.ToString()}], a hashing service with this algorithm does not exist.");
            }
        }
    }
}
