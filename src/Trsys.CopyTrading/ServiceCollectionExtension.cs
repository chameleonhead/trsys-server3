using Microsoft.Extensions.DependencyInjection;

namespace Trsys.CopyTrading
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCopyTrading(this IServiceCollection services)
        {
            services.AddSingleton<CopyTradingEventFlowRootResolver>();
            services.AddSingleton<ICopyTradingService, CopyTradingService>();
            return services;
        }
    }
}
