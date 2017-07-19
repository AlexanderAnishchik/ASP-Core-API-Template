using BusinessComponents.Services;
using DataAccess.DatabaseInitializer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace DependencyInjections.BusinessComponentsDIExteinsions
{
    //
    // Summary:
    //     Extension methods for setting up EF context in an Microsoft.Extensions.DependencyInjection.IServiceCollection.
    public static class BusinessComponentsServiceCollectionExtensions
    {
        public static void AddBusinessComponents(this IServiceCollection services)
        {
            services.AddTransient<IPostService, PostService>();
        }
    }
}
