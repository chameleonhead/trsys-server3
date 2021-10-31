using Microsoft.Extensions.DependencyInjection;
using Trsys.BackOffice.Infrastructure;

namespace Trsys.BackOffice
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddBackOffice(this IServiceCollection services)
        {
            services.AddBackOfficeInfrastructure();
            return services;
        }
    }
}
