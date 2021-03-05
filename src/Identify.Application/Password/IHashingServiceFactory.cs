using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identify.Application.Password
{
    public interface IHashingServiceFactory
    {
        Task<IHashingService> CreateHashingService(HashingAlgorithm algorithm);
    }
}
