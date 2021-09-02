using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCCMasiv.Utils
{
    public static class Utils
    {
        public static string CreateMessageError(string message)
        {
            return JsonConvert.SerializeObject(
                new ResponseError
                {
                    Message = message,
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                }
                );
        }
    }
}
