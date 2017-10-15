﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Entities;
using BusinessComponents.Services;
using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using Project.WebAPI;
using Microsoft.Extensions.Options;
using Project.Domain.Identity;
using Project.Services.AuthorizationService;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IPostService _postService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly IOptions<JWTOptions> _jwtAccessor;
        private readonly IConsumerAuthorizationService _authorizationService;
        public AccountController(IPostService postService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IPasswordHasher<ApplicationUser> passwordHasher,
            IOptions<JWTOptions> jwtAccessor,
            RoleManager<IdentityRole> roleManager,
            IConsumerAuthorizationService authorizationService
            )
        {
            _postService = postService;
            _userManager = userManager;
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
            _jwtAccessor = jwtAccessor;
            _roleManager = roleManager;
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
