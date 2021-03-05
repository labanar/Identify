using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identify.Application.Password
{
    public interface IPasswordValidator
    {
        Task<bool> ValidateAsync(string username, string password);
    }
}
