using EventFlow;
using EventFlow.Extensions;
using System.Collections.Generic;
using Trsys.CopyTrading.Application.Read.Models;
using Trsys.CopyTrading.Application.Read.Queries;
using Trsys.CopyTrading.Application.Write.Commands;
using Trsys.CopyTrading.Application.Write.Sagas;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application
{
    public static class EventFlowOptionsExtension
    {
        public static IEventFlowOptions UseApplication(this IEventFlowOptions options)
        {
            options
                .AddCommands(
                    typeof(AccountStateUpdateCommand),
                    typeof(AddSubscriberCommand),
                    typeof(PublishOrderOpenCommand),
                    typeof(StartTradeDistributionCommand),
                    typeof(AddCopyTradeDistributedAccountCommand),
                    typeof(DistributeOpenTradeCommand),
                    typeof(PublishOrderCloseCommand),
                    typeof(DistributeCloseTradeCommand)
                )
                .AddCommandHandlers(
                    typeof(AccountStateUpdateCommandHandler),
                    typeof(AddSubscriberCommandHandler),
                    typeof(PublishOrderOpenCommandHandler),
                    typeof(StartTradeDistributionCommandHandler),
                    typeof(AddCopyTradeDistributedAccountCommandHandler),
                    typeof(DistributeOpenTradeCommandHandler),
                    typeof(PublishOrderCloseCommandHandler),
                    typeof(DistributeCloseTradeCommandHandler)
                )
                .AddEvents(
                    typeof(AccountStateUpdatedEvent),
                    typeof(SubscriptionAddedEvent),
                    typeof(CopyTradeOpenedEvent),
                    typeof(TradeDistributionStartedEvent),
                    typeof(TradeOrderOpenedEvent),
                    typeof(CopyTradeApplicantAddedEvent),
                    typeof(TradeOrderOpenDistributedEvent),
                    typeof(CopyTradeClosedEvent),
                    typeof(TradeOrderClosedEvent),
                    typeof(TradeOrderCloseDistributedEvent),
                    typeof(TradeOrderInactivatedEvent)
                )
                .AddSagaLocators(
                    typeof(TradeDistributionSagaLocator)
                )
                .AddSagas(
                    typeof(TradeDistributionSaga)
                )
                .AddEvents(
                    typeof(TradeDistributionSagaStartedEvent),
                    typeof(TradeDistributionSagaFinishedEvent)
                );
            options
                .UseInMemoryReadStoreFor<AccountReadModel>()
                .UseInMemoryReadStoreFor<CopyTradeReadModel>()
                .UseInMemoryReadStoreFor<TradeOrderReadModel, TradeOrderReadModelLocator>()
                .RegisterServices(sr => sr.RegisterType(typeof(TradeOrderReadModelLocator)))
                .AddQueryHandler<TradeOrderReadModelAllQueryHandler, TradeOrderReadModelAllQuery, IReadOnlyCollection<TradeOrderReadModel>>();
            return options;
        }
    }
}
