using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identify.Web.Api.Responses
{
    public class ActionResponse: IApiResponse
    {
        public bool Success { get; set; }
    }
}
