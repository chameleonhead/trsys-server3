using Microsoft.Extensions.DependencyInjection;
using Trsys.Frontend.Infrastructure;

namespace Trsys.Frontend
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddFrontend(this IServiceCollection services)
        {
            services.AddFrontendInfrastructure();
            return services;
        }
    }
}
