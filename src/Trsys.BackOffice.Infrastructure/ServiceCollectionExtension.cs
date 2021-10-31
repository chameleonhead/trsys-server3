using Microsoft.Extensions.DependencyInjection;
using Trsys.BackOffice.Abstractions;

namespace Trsys.BackOffice.Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddBackOfficeInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<BackOfficeEventFlowRootResolver>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IDistributionGroupService, DistributionGroupService>();
            services.AddSingleton<IPublisherService, PublisherService>();
            services.AddSingleton<ISubscriberService, SubscriberService>();
            services.AddSingleton<ICopyTradeService, CopyTradeService>();

            services.AddHostedService<CopyTradingEventHandler>();
            services.AddHostedService<EaEventHandler>();
            return services;
        }
    }
}
