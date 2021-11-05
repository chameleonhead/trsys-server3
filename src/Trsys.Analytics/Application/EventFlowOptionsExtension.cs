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
                    typeof(CopyTradeOpenCommand),
                    typeof(CopyTradeCloseCommand),
                    typeof(PublisherOpenCopyTradeCommand),
                    typeof(PublisherCloseCopyTradeCommand)
                )
                .AddCommandHandlers(
                    typeof(CopyTradeOpenCommandHandler),
                    typeof(CopyTradeCloseCommandHandler),
                    typeof(PublisherOpenCopyTradeCommandHandler),
                    typeof(PublisherCloseCopyTradeCommandHandler)
                )
                .AddEvents(
                    typeof(CopyTradeOpenedEvent),
                    typeof(CopyTradeClosedEvent),
                    typeof(PublisherOpenedCopyTradeEvent),
                    typeof(PublisherClosedCopyTradeEvent)
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
