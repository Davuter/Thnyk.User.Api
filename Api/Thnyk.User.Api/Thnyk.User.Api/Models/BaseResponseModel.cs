using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thnyk.User.Api.Models
{
    public class BaseResponseModel
    {
        public BaseResponseModel()
        {
            Success = 0;
        }
        public int Success { get; set; }
        public string Message { get; set; }
        public string ExtraFields { get; set; }
    }
}
