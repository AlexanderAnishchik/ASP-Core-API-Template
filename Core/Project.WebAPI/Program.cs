using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Domain.Models;
using Microsoft.Extensions.Logging;
using Project.Domain.Identity;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
         //   EnsureDatabaseInitialized(host);
            host.Run();

        }
        public static IWebHost BuildWebHost(string[] args) =>
              WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        public static void EnsureDatabaseInitialized(IWebHost host) {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    IdentityExtensionService.EnsureRolesCreated(services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }
        }
    }
}
