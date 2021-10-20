using EventFlow.AspNetCore.Extensions;
using EventFlow.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Trsys.CopyTrading.Application;
using Trsys.Ea.Application;

namespace Trsys.Ea
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddEa(this IServiceCollection services)
        {
            services.AddEventFlow(ef =>
            {
                ef.UseCopyTradeApplication();
                ef.UseEaApplication();
                ef.AddAspNetCore();
            });
            services.AddSingleton<IEaService, EaService>();
            services.AddSingleton<IEaSessionTokenProvider, EaSessionTokenProvider>();
            services.AddSingleton<IEaSessionTokenParser, EaSessionTokenParser>();
            services.AddSingleton<IEaSessionManager, EaSessionManager>();
            services.AddSingleton<IEaSessionStore, EaSessionStore>();
            return services;
        }
    }
}
