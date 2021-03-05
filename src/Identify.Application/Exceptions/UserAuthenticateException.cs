using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Identify.Application.Exceptions
{
    public class UserAuthenticateException : Exception
    {
        public UserAuthenticateException()
        {
        }

        public UserAuthenticateException(string message) : base(message)
        {
        }

        public UserAuthenticateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserAuthenticateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
