using EventFlow;
using EventFlow.Extensions;
using Trsys.Analytics.Application.Read.Models;
using Trsys.Analytics.Application.Write.Commands;
using Trsys.Analytics.Domain;

namespace Trsys.Analytics.Application
{
    public static class EventFlowOptionsExtension
    {
        public static IEventFlowOptions UseAnalyticsApplication(this IEventFlowOptions options)
        {
            options
                .AddCommands(
                    typeof(CopyTradeOpenCommand)
                )
                .AddCommandHandlers(
                    typeof(CopyTradeOpenCommandHandler)
                )
                .AddEvents(
                    typeof(CopyTradeOpenedEvent),
                    typeof(CopyTradeClosedEvent)
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
