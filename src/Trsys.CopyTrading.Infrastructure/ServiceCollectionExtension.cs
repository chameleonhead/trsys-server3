using Microsoft.Extensions.DependencyInjection;
using Trsys.CopyTrading.Abstractions;

namespace Trsys.CopyTrading.Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCopyTradingInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<ICopyTradingEventBus, AllEventBus>();
            services.AddSingleton<ICopyTradingService, CopyTradingService>();
            services.AddSingleton<CopyTradingEventFlowRootResolver>();
            return services;
        }
    }
}
