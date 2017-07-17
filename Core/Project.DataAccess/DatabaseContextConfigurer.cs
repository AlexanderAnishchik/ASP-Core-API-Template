using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore.Extensions;
using System;

namespace DataAccess.DatabaseInitializer
{
    public static class DatabaseContextConfigurer
    {
        public static void SetupSqlServer(DbContextOptionsBuilder builder, String connectionString)
        {
            builder.UseSqlServer(connectionString);
        }
        public static void SetupMySql(DbContextOptionsBuilder builder, String connectionString)
        {
            builder.UseMySQL(connectionString);
        }
        public static void SetupSqlite(DbContextOptionsBuilder builder, String connectionString)
        {
            builder.UseSqlite(connectionString);
        }
        public static void SetupNpgsql(DbContextOptionsBuilder builder, String connectionString)
        {
            builder.UseNpgsql(connectionString);
        }
    }
}
