using Microsoft.Extensions.DependencyInjection;

namespace Trsys.BackOffice
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddBackOffice(this IServiceCollection services)
        {
            services.AddSingleton<BackOfficeEventFlowRootResolver>();
            services.AddSingleton<IBackOfficeService, BackOfficeService>();
            return services;
        }
    }
}
