using Microsoft.Extensions.DependencyInjection;
using Trsys.Analytics.Abstractions;

namespace Trsys.Analytics.Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddAnalyticsInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<AnalyticsEventFlowRootResolver>();
            services.AddSingleton<IAnalyticsService, AnalyticsService>();
            return services;
        }
    }
}
