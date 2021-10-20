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
                ef.UseCopyTradeApplication();
                ef.AddAspNetCore();
            });
            return services;
        }
    }
}
