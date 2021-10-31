using Microsoft.Extensions.DependencyInjection;
using Trsys.CopyTrading.Infrastructure;

namespace Trsys.CopyTrading
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCopyTrading(this IServiceCollection services)
        {
            services.AddCopyTradingInfrastructure();
            return services;
        }
    }
}
