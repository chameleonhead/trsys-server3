using Microsoft.Extensions.DependencyInjection;
using Trsys.Ea.Infrastructure;

namespace Trsys.Ea
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddEa(this IServiceCollection services)
        {
            services.AddEaInfrastructure();
            return services;
        }
    }
}
