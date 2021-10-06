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
                    typeof(PublishOrderOpenCommand)
                )
                .AddCommandHandlers(
                    typeof(PublishOrderOpenCommandHandler)
                )
                .AddEvents(
                    typeof(CopyTradeOpenedEvent)
                );
            return options;
        }
    }
}
