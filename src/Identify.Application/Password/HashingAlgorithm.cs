using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identify.Application.Password
{
    public enum HashingAlgorithm
    {
        None = 0,
        PBKDF2_HMACSHA256_1000,
        PBKDF2_HMACSHA512_1000,
        PBKDF2_HMACSHA1_1000,
        BCRYPT
    }
}
