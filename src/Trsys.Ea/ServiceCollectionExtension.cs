using Microsoft.Extensions.DependencyInjection;

namespace Trsys.Ea
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddEa(this IServiceCollection services)
        {
            services.AddSingleton<IEaService, EaService>();
            services.AddSingleton<IEaSessionTokenProvider, EaSessionTokenProvider>();
            services.AddSingleton<IEaSessionTokenParser, EaSessionTokenParser>();
            services.AddSingleton<IEaSessionManager, EaSessionManager>();
            services.AddSingleton<IEaSessionStore, EaSessionStore>();
            return services;
        }
    }
}
