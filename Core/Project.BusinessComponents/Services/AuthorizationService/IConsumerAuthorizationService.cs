using Microsoft.AspNetCore.Identity;
using Project.Services.Models;
using System.Threading.Tasks;

namespace Project.Services.AuthorizationService
{
    public interface IConsumerAuthorizationService
    {
        Task<IdentityResult> RegisterUserAsync(AuthModel model);
        Task<JWTTokenStatusResult> GenerateTokenAsync(AuthModel model, string secret, string issuer, string audience);
    }
}
