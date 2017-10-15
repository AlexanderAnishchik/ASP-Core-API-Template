using Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Domain.Identity
{
    public static class IdentityExtensionService
    {
        public static void EnsureRolesCreated(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<UtilitiesContext>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            if (context == null || roleManager == null)
            {
                throw new InvalidCastException();

            }
            foreach (var role in Roles.All)
            {
                if (!roleManager.RoleExistsAsync(role.ToUpper()).Result)
                {
                    roleManager.CreateAsync(new IdentityRole() { Name = role });
                }
            }
            context.SaveChanges();
        }
    }
}
