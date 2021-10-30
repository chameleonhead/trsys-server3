using Microsoft.Extensions.DependencyInjection;
using Trsys.Ea.Application;

namespace Trsys.Ea
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddEa(this IServiceCollection services)
        {
            services.AddHostedService<CopyTradingEventHandler>();
            services.AddSingleton<ICopyTradingService, EaCopyTradingService>();
            services.AddSingleton<IEaService, EaService>();
            services.AddSingleton<IEaSessionTokenProvider, EaSessionTokenProvider>();
            services.AddSingleton<IEaSessionTokenParser, EaSessionTokenParser>();
            services.AddSingleton<IEaSessionManager, EaSessionManager>();
            services.AddSingleton<IEaSessionStore, EaSessionStore>();
            return services;
        }
    }
}
