using BusinessComponents.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Project.Services.AuthorizationService;
using System;
using System.Collections.Generic;

namespace WebApi.DependencyInjections.ServiceComponents
{
    public static class ServiceComponents
    {
        public static void AddBusinessComponents(this IServiceCollection services)
        {
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IConsumerAuthorizationService, ConsumerAuthorizationService>();
        }
    }
}
