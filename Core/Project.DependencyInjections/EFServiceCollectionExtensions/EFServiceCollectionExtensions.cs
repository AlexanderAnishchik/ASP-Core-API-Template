using DataAccess.DatabaseInitializer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace DependencyInjections.DatabaseInitializer
{
    //
    // Summary:
    //     Extension methods for setting up EF context in an Microsoft.Extensions.DependencyInjection.IServiceCollection.
    public static class EFServiceCollectionExtensions
    {
        private static Dictionary<DatabaseProvidersEnum, Action<DbContextOptionsBuilder, String>> dbProviders = new Dictionary<DatabaseProvidersEnum, Action<DbContextOptionsBuilder, String>>(){
            {DatabaseProvidersEnum.SqlServer, DatabaseContextConfigurer.SetupSqlServer},
            {DatabaseProvidersEnum.MySql, DatabaseContextConfigurer.SetupMySql},
            {DatabaseProvidersEnum.SqLite, DatabaseContextConfigurer.SetupSqlite},
            {DatabaseProvidersEnum.PostgreSQL, DatabaseContextConfigurer.SetupNpgsql}
        };
        public static void AddDatabaseContext(this IServiceCollection services, DatabaseProvidersEnum provider, String connectionString)
        {
            services.AddDbContext<DataAccess.Models.UtilitiesContext>(t => GetProvider(provider, t, connectionString));
        }
        private static void GetProvider(DatabaseProvidersEnum provider, DbContextOptionsBuilder builder, String connectionString)
        {
            Action<DbContextOptionsBuilder, String> initDatabase = null;
            if (dbProviders.TryGetValue(provider, out initDatabase))
            {
                initDatabase(builder, connectionString);
            }
        }
    }
}
