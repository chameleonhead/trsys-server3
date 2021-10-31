using Microsoft.Extensions.DependencyInjection;
using Trsys.CopyTrading.Abstractions;

namespace Trsys.CopyTrading.Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCopyTradingInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<CopyTradingEventFlowRootResolver>();
            services.AddSingleton<ICopyTradingService, CopyTradingService>();
            return services;
        }
    }
}
