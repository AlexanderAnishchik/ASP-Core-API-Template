using BusinessComponents.DatabaseInitializer;
using BusinessComponents.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
