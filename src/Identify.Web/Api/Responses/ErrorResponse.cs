using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identify.Web.Api.Responses
{
    public class ErrorResponse: IApiResponse
    {
        public ErrorResponse(IEnumerable<string> errors)
        {
            Errors = errors ?? new string[] { };
        }

        public bool Success => false;
        public IEnumerable<string> Errors { get; private set; }
    }
}
