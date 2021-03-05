using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identify.Application.Password
{
    public class Hash
    {
        public static Hash Empty => new Hash
        {
            HashedValue = string.Empty,
            Salt = string.Empty,
            HashingAlgorithm = HashingAlgorithm.None
        };

        public string HashedValue { get; set; }
        public string Salt { get; set; }
        public HashingAlgorithm HashingAlgorithm { get; set; }
    }
}
