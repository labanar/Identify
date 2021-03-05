using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Identify.Application.Password
{
    public class Pbkdf2HashingService_HMACSHA256_1000 : IHashingService
    {
        public Pbkdf2HashingService_HMACSHA256_1000()
        {

        }

        public Task<string> ComputeHash(string plainText, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: plainText,
            salt: saltBytes,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 1000,
            numBytesRequested: 256 / 8));

            return Task.FromResult(hashed);
        }

        public Task<Hash> CreateHash(string plainText)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: plainText,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 1000,
            numBytesRequested: 256 / 8));


            return Task.FromResult(new Hash
            {
                HashedValue = hashed,
                Salt = Convert.ToBase64String(salt),
                HashingAlgorithm = HashingAlgorithm.PBKDF2_HMACSHA256_1000
            });
        }
    }
}
