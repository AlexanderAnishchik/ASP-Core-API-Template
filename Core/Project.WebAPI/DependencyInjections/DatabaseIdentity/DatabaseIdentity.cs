using Domain;
using Domain.DatabaseInitializer;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace WebApi.DependencyInjections.DatabaseIdentity
{
    //
    // Summary:
    //     Extension methods for setting up EF context in an Microsoft.Extensions.DependencyInjection.IServiceCollection.
    public static class DatabaseIdentity
    {
        private static Dictionary<DatabaseProvidersEnum, Action<DbContextOptionsBuilder, String>> dbProviders = new Dictionary<DatabaseProvidersEnum, Action<DbContextOptionsBuilder, String>>(){
            {DatabaseProvidersEnum.SqlServer, DatabaseContextConfiguration.SetupSqlServer},
            {DatabaseProvidersEnum.MySql, DatabaseContextConfiguration.SetupMySql},
            {DatabaseProvidersEnum.SqLite, DatabaseContextConfiguration.SetupSqlite},
            {DatabaseProvidersEnum.PostgreSQL, DatabaseContextConfiguration.SetupNpgsql}
        };
        public static void AddDatabaseContext(this IServiceCollection services, DatabaseProvidersEnum provider, String connectionString)
        {
           
            
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
