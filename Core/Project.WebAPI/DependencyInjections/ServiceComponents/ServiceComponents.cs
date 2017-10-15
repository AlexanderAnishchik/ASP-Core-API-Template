using Microsoft.Extensions.DependencyInjection;
using Project.Services.AuthorizationService;

namespace WebApi.DependencyInjections.ServiceComponents
{
    public static class ServiceComponents
    {
        public static void AddBusinessComponents(this IServiceCollection services)
        {
            services.AddTransient<IConsumerAuthorizationService, ConsumerAuthorizationService>();
        }
    }
}
