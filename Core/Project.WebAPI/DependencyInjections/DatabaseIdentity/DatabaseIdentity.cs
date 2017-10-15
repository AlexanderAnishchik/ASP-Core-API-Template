using Domain;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Project.Domain.Identity;
using System;
using System.Collections.Generic;

namespace WebApi.DependencyInjections.DatabaseIdentity
{
    //
    // Summary:
    //     Extension methods for setting up EF context in an Microsoft.Extensions.DependencyInjection.IServiceCollection.
    public static class DatabaseIdentity
    {

        public static void SetupIdentity(this IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            IdentityBuilder builder = services.AddIdentityCore<ApplicationUser>(opt =>
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequiredLength = 8;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireLowercase = true;
            }
        );
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            builder.AddEntityFrameworkStores<UtilitiesContext>();
            builder.AddRoleValidator<RoleValidator<IdentityRole>>();
            builder.AddRoleManager<RoleManager<IdentityRole>>();
            builder.AddSignInManager<SignInManager<ApplicationUser>>();

        }
    }
}
