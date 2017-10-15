using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Project.Services.Models
{
   public class JWTTokenStatusResult
    {
        public JwtSecurityToken token { get; internal set; }
        public Boolean IsAuthorized { get; internal set; }
    }
}
