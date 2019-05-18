using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOPTut.Web.Api.Models
{
    public class JwtTokenResult
    {
        public string AccessToken { get; set; }
        public int ExpireInSeconds { get; set; }
        public string UserId { get; set; }

    }
}
