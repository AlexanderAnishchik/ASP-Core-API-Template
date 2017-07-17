using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Angular4.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.Extensions.FileProviders;
using MySQL.Data.EntityFrameworkCore.Extensions;
using Angular4.Services.DB;
using Services.Services;
using Services.DatabaseInitializer;

namespace Angular4
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
           var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.AddTransient<IPostService, PostService>();
            services.AddDatabaseContext(DatabaseProvidersEnum.SqlServer,Configuration.GetConnectionString("SQLServerConnection"));


            // services.AddDatabaseContext<UtilitiesContext>(options => options.UseMySQL(Configuration.GetConnectionString("MySQLConnection")));
            // services.AddDatabaseContext<UtilitiesContext>(options => options.UseSqlite(Configuration.GetConnectionString("SQLiteConnection")));
            //services.AddDatabaseContext<UtilitiesContext>(options => options.UseNpgsql(Configuration.GetConnectionString("PostgreSQLConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}
