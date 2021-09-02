using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TestCCMasiv.Utils
{
    public class ResponseError
    {
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
