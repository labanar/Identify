using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Identify.Application.Exceptions
{
    public class UserActivationException : Exception
    {
        public UserActivationException()
        {
        }

        public UserActivationException(string message) : base(message)
        {
        }

        public UserActivationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserActivationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
