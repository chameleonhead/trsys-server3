using Microsoft.Extensions.DependencyInjection;
using Trsys.Analytics.Infrastructure;

namespace Trsys.Analytics
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddAnalytics(this IServiceCollection services)
        {
            services.AddAnalyticsInfrastructure();
            return services;
        }
    }
}
