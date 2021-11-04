using EventFlow;
using EventFlow.Extensions;
using System.Collections.Generic;
using Trsys.Analytics.Application.Read.Models;

namespace Trsys.Analytics.Application
{
    public static class EventFlowOptionsExtension
    {
        public static IEventFlowOptions UseAnalyticsApplication(this IEventFlowOptions options)
        {
            options
                .AddCommands(
                )
                .AddCommandHandlers(
                )
                .AddEvents(
                );
            options
                .RegisterServices(sr =>
                {
                    sr.RegisterType(typeof(CopyTradeReadModelLocator));
                })
                .UseInMemoryReadStoreFor<CopyTradeReadModel, CopyTradeReadModelLocator>();
            return options;
        }
    }
}
