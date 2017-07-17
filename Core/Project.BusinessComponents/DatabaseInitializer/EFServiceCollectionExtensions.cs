using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySQL.Data.EntityFrameworkCore.Extensions;
using System;

namespace BusinessComponents.DatabaseInitializer
{
    //
    // Summary:
    //     Extension methods for setting up EF context in an Microsoft.Extensions.DependencyInjection.IServiceCollection.
    public static class EFServiceCollectionExtensions
    {

        public static void AddDatabaseContext(this IServiceCollection services, DatabaseProvidersEnum provider,String connectionString) {
            services.AddDbContext<UtilitiesContext>(t=> GetProvider(provider,t, connectionString));
        }
        private static void GetProvider(DatabaseProvidersEnum provider,DbContextOptionsBuilder builder, String connectionString) {
            switch (provider)
            {
                case DatabaseProvidersEnum.SqlServer:
                    {
                        builder.UseSqlServer(connectionString);
                        break;
                    }
                case DatabaseProvidersEnum.MySql:
                    {
                        builder.UseMySQL(connectionString);
                        break;
                    }
                case DatabaseProvidersEnum.SqLite:
                    {
                        builder.UseSqlite(connectionString);
                        break;
                    }
                case DatabaseProvidersEnum.PostgreSQL:
                    {
                        builder.UseNpgsql(connectionString);
                        break;
                    }
            }
        }
    }
}
