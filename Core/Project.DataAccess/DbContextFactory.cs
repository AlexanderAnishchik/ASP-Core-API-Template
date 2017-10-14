using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Project.Domain
{
    /// <summary>
    /// UtilitiesDbContextFactory is used by dotnet ef migrations and database operations. ( If Main.BuildWebHost method is not available )
    /// </summary>
    public class UtilitiesDbContextFactory : IDesignTimeDbContextFactory<UtilitiesContext>
    {
        public UtilitiesContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<UtilitiesContext>();

            IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.Development.json", optional: false)
            .Build();

            builder.UseSqlServer(configuration.GetConnectionString("SQLServerConnection"),
                optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(UtilitiesContext).GetTypeInfo().Assembly.GetName().Name));
            return new UtilitiesContext(builder.Options);
        }
    }
}
