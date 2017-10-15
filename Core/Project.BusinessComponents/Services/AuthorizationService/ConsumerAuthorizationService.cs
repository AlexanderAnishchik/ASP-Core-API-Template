using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Domain.Models;
using Microsoft.Extensions.Options;
using Project.Domain.Identity;
using Project.Services.Models;
using System.Linq;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Project.Services.AuthorizationService
{
    public class ConsumerAuthorizationService : IConsumerAuthorizationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        public readonly UtilitiesContext _context;

        public ConsumerAuthorizationService(
            UserManager<ApplicationUser> userManager,
            IPasswordHasher<ApplicationUser> passwordHasher,
            RoleManager<IdentityRole> roleManager,
            UtilitiesContext context
            )
        {
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _roleManager = roleManager;
            _context = context;
        }
        public async Task<JWTTokenStatusResult> GenerateTokenAsync(AuthModel model, string secret, string issuer, string audience)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                return new JWTTokenStatusResult() { token = null, IsAuthorized = false };
            }
            if (_passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) == PasswordVerificationResult.Success)
            {
                var userClaims = await _userManager.GetClaimsAsync(user);
                var roles = await _userManager.GetRolesAsync(user);
                var rolesStr = string.Join(",", roles.ToList());
                var claimsWithRoles = roles.Select(x => new Claim(ClaimTypes.Role, x));
                var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                }.Union(userClaims).Union(claimsWithRoles);
                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
                var signinCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
                var jwtSecurityToken = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,

                    claims: claims,
                    expires: DateTime.UtcNow.AddYears(1),
                    signingCredentials: signinCredentials);
                return new JWTTokenStatusResult() { token = jwtSecurityToken, IsAuthorized = true };
            }
            else
            {
                return new JWTTokenStatusResult() { token = null, IsAuthorized = false };
            }
        }

        public async Task<IdentityResult> RegisterUserAsync(AuthModel model)
        {
            IdentityResult addUserResult = default(IdentityResult);
            IdentityResult addRoleResult = default(IdentityResult);
            IdentityResult response = default(IdentityResult);
            using (var contextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                    addUserResult = await _userManager.CreateAsync(user, model.Password);
                    if (!addUserResult.Succeeded) { response = addUserResult; }
                    else
                    {
                        addRoleResult = await _userManager.AddToRoleAsync(user, Roles.Basic);
                        response = addRoleResult;
                    }
                    contextTransaction.Commit();
                }
                catch (Exception)
                {
                    contextTransaction.Rollback();
                }
            }
            return response;
        }
    }
}
