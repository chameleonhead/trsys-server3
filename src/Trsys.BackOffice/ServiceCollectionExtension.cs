using Microsoft.Extensions.DependencyInjection;

namespace Trsys.BackOffice
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddBackOffice(this IServiceCollection services)
        {
            services.AddSingleton<BackOfficeEventFlowRootResolver>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IDistributionGroupService, DistributionGroupService>();
            services.AddSingleton<IPublisherService, PublisherService>();
            services.AddSingleton<ISubscriberService, SubscriberService>();
            services.AddSingleton<ICopyTradeService, CopyTradeService>();
            return services;
        }
    }
}
