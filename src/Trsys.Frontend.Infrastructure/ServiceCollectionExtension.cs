using Microsoft.Extensions.DependencyInjection;
using Trsys.Frontend.Abstractions;

namespace Trsys.Frontend.Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddFrontendInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<EaEventFlowRootResolver>();
            services.AddSingleton<IEaService, EaService>();
            services.AddSingleton<IEaSessionTokenProvider, EaSessionTokenProvider>();
            services.AddSingleton<IEaSessionTokenParser, EaSessionTokenParser>();
            services.AddSingleton<IEaSessionManager, EaSessionManager>();
            services.AddSingleton<IEaSessionStore, EaSessionStore>();
            
            services.AddSingleton<IEaEventBus, AllEventBus>();

            services.AddHostedService<CopyTradingEventHandler>();
            return services;
        }
    }
}
