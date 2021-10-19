using EventFlow.AspNetCore.Extensions;
using EventFlow.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Trsys.CopyTrading.Application;

namespace Trsys.CopyTrading
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCopyTrading(this IServiceCollection services)
        {
            services.AddEventFlow(ef =>
            {
                ef.UseApplication();
                ef.AddAspNetCore();
            });
            services.AddSingleton<IEaService, EaService>();
            services.AddSingleton<IEaSessionTokenProvider, EaSessionTokenProvider>();
            services.AddSingleton<IEaSessionTokenParser, EaSessionTokenParser>();
            services.AddSingleton<IEaSessionManager, EaSessionManager>();
            return services;
        }
    }
}
