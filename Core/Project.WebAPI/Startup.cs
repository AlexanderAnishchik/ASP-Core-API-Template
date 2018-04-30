using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using WebApi.DependencyInjections.ServiceComponents;
using WebApi.DependencyInjections.DatabaseIdentity;
using Domain.Models;
using Project.WebAPI;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Project.Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme() { In = "header", Description = "Please insert JWT with Bearer into field", Name = "Authorization", Type = "apiKey" });
            });

            services.Configure<JWTOptions>(Configuration);

            // Setup services
            services.AddBusinessComponents();

            // Setup database context

            services.AddDbContext<UtilitiesContext>(provider => provider.UseMySql(Configuration.GetConnectionString("MySQLConnection")));


            // Setup Identity (UserManager, Roles etc.)
            services.SetupIdentity();

            //Setup JWT Authorization
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.IncludeErrorDetails = true;
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateActor = false,
                    ValidIssuer = Configuration["secretJWTKeyIssuer"],
                    ValidAudience = Configuration["secretJWTKeyAudience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["secretJWTKey"])),
                    ValidateLifetime = true,
                };

            });
            // Setup MVC services (Route Handling, CORS etc.)
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

        }
    }
}
