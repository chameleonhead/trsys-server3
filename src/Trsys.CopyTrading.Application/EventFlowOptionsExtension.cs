using EventFlow;
using EventFlow.Extensions;
using System.Collections.Generic;
using Trsys.CopyTrading.Application.Read.Models;
using Trsys.CopyTrading.Application.Read.Queries;
using Trsys.CopyTrading.Application.Write.Commands;
using Trsys.CopyTrading.Application.Write.Sagas.TradeDistribution;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application
{
    public static class EventFlowOptionsExtension
    {
        public static IEventFlowOptions UseCopyTradeApplication(this IEventFlowOptions options)
        {
            options
                .AddCommands(
                    typeof(DistributionGroupAddSubscriberCommand),
                    typeof(DistributionGroupRemoveSubscriberCommand),
                    typeof(DistributionGroupPublishOpenCommand),
                    typeof(DistributionGroupPublishCloseCommand),
                    typeof(CopyTradeOpenCommand),
                    typeof(CopyTradeCloseCommand)
                )
                .AddCommandHandlers(
                    typeof(DistributionGroupAddSubscriberCommandHandler),
                    typeof(DistributionGroupRemoveSubscriberCommandHandler),
                    typeof(DistributionGroupPublishOpenCommandHandler),
                    typeof(DistributionGroupPublishCloseCommandHandler),
                    typeof(CopyTradeOpenCommandHandler),
                    typeof(CopyTradeCloseCommandHandler)
                )
                .AddEvents(
                    typeof(DistributionGroupSubscriberAddedEvent),
                    typeof(DistributionGroupSubscriberRemovedEvent),
                    typeof(DistributionGroupOpenPublishedEvent),
                    typeof(DistributionGroupClosePublishedEvent),
                    typeof(CopyTradeOpenedEvent),
                    typeof(CopyTradeClosedEvent)
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
                .UseInMemoryReadStoreFor<DistributionGroupReadModel>()
                .UseInMemoryReadStoreFor<CopyTradeReadModel>()
                .AddQueryHandler<CopyTradeReadModelAllQueryHandler, CopyTradeReadModelAllQuery, IReadOnlyCollection<CopyTradeReadModel>>();
            return options;
        }
    }
}
