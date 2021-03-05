using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identify.Web.Api.Responses
{
    public class DataResponse<T>: IApiResponse
    {
        public DataResponse(T data)
        {
            Data = data;
        }
        public bool Success => true;
        public T Data { get; set; }
    }
}
