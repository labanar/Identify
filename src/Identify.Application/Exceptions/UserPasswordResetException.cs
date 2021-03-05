using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identify.Application.Exceptions
{
    public class UserPasswordResetException : Exception
    {
        public UserPasswordResetException(string message) : base(message)
        {
        }
    }
}
