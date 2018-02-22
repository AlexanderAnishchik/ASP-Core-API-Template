using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Project.WebAPI;
using Microsoft.Extensions.Options;
using Project.Services.AuthorizationService;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {

        private readonly IOptions<JWTOptions> _jwtAccessor;
        private readonly IConsumerAuthorizationService _authorizationService;
        public AccountController(IOptions<JWTOptions> jwtAccessor, IConsumerAuthorizationService authorizationService)
        {
            _jwtAccessor = jwtAccessor;
            _authorizationService = authorizationService;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]AuthModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authorizationService.RegisterUserAsync(model);
            return result.Succeeded ? (IActionResult)Ok() : (IActionResult)BadRequest("Invalid login attempt.");
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("token")]
        public async Task<IActionResult> Login([FromBody]AuthModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authorizationService.GenerateTokenAsync(model, _jwtAccessor.Value.secretJWTKey, _jwtAccessor.Value.secretJWTKeyIssuer, _jwtAccessor.Value.secretJWTKeyAudience);
            if (!result.IsAuthorized) { return Unauthorized(); }
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(result.token),
                expiration = result.token.ValidTo

            });
        }
    }
}

