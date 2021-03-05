using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identify.Application.Exceptions
{
    public class UserGrantConsentException : Exception
    {
        public UserGrantConsentException(string message) : base(message)
        {
        }
    }
}
