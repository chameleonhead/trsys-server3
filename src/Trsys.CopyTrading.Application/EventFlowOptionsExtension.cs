using EventFlow;
using EventFlow.Extensions;
using Trsys.CopyTrading.Application.Write;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application
{
    public static class EventFlowOptionsExtension
    {
        public static IEventFlowOptions UseApplication(this IEventFlowOptions options)
        {
            options
                .AddCommands(
                    typeof(AddSubscriberCommand),
                    typeof(PublishOrderOpenCommand)
                )
                .AddCommandHandlers(
                    typeof(AddSubscriberCommandHandler),
                    typeof(PublishOrderOpenCommandHandler)
                )
                .AddEvents(
                    typeof(SubscriptionAddedEvent),
                    typeof(CopyTradeOpenedEvent)
                );
            return options;
        }
    }
}
