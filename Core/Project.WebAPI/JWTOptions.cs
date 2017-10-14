using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.WebAPI
{
    public class JWTOptions
    {
        public string secretJWTKey { get; set; }
        public string secretJWTKeyIssuer { get; set; }
        public string secretJWTKeyAudience { get; set; }
    }
}
