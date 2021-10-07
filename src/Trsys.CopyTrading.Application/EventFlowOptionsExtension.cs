using EventFlow;
using EventFlow.Extensions;
using System.Collections.Generic;
using Trsys.CopyTrading.Application.Read;
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
                    typeof(PublishOrderOpenCommand),
                    typeof(StartTradeDistributionCommand),
                    typeof(OpenTradeCommand)
                )
                .AddCommandHandlers(
                    typeof(AddSubscriberCommandHandler),
                    typeof(PublishOrderOpenCommandHandler),
                    typeof(StartTradeDistributionCommandHandler),
                    typeof(OpenTradeCommandHandler)
                )
                .AddEvents(
                    typeof(SubscriptionAddedEvent),
                    typeof(CopyTradeOpenedEvent),
                    typeof(TradeDistributionStartedEvent),
                    typeof(TradeOrderOpenedEvent)
                )
                .AddSagaLocators(
                    typeof(TradeDistributionSagaLocator)
                )
                .AddSagas(
                    typeof(TradeDistributionSaga)
                );
            options
                .UseInMemoryReadStoreFor<CopyTradeReadModel>()
                .UseInMemoryReadStoreFor<TradeOrderReadModel, TradeOrderReadModelLocator>()
                .RegisterServices(sr => sr.RegisterType(typeof(TradeOrderReadModelLocator)))
                .AddQueryHandler<TradeOrderReadModelAllQueryHandler, TradeOrderReadModelAllQuery, IReadOnlyCollection<TradeOrderReadModel>>();
            return options;
        }
    }
}
