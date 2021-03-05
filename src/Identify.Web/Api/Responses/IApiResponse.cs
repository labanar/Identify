using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identify.Web.Api.Responses
{
    public interface IApiResponse
    {
        public bool Success { get; }
    }
}
