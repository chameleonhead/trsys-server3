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
                    typeof(AccountStateUpdateCommand),
                    typeof(AccountRequestOpenTradeOrderCommand),
                    typeof(AccountRequestCloseTradeOrderCommand),
                    typeof(AccountDistributeOpenTradeOrderRequestCommand),
                    typeof(AccountDistributeCloseTradeOrderRequestCommand),
                    typeof(DistributionGroupAddPublisherCommand),
                    typeof(DistributionGroupAddSubscriberCommand),
                    typeof(DistributionGroupPublishOpenCommand),
                    typeof(DistributionGroupPublishCloseCommand),
                    typeof(CopyTradeOpenCommand),
                    typeof(CopyTradeAddDistributedAccountCommand),
                    typeof(CopyTradeCloseCommand),
                    typeof(CopyTradeRemoveDistributedAccountCommand)
                )
                .AddCommandHandlers(
                    typeof(AccountStateUpdateCommandHandler),
                    typeof(AccountRequestOpenTradeOrderCommandHandler),
                    typeof(AccountRequestCloseTradeOrderCommandHandler),
                    typeof(AccountDistributeOpenTradeOrderRequestCommandHandler),
                    typeof(AccountDistributeCloseTradeOrderRequestCommandHandler),
                    typeof(DistributionGroupAddPublisherCommandHandler),
                    typeof(DistributionGroupAddSubscriberCommandHandler),
                    typeof(DistributionGroupPublishOpenCommandHandler),
                    typeof(DistributionGroupPublishCloseCommandHandler),
                    typeof(CopyTradeOpenCommandHandler),
                    typeof(CopyTradeCloseCommandHandler),
                    typeof(CopyTradeAddDistributedAccountCommandHandler),
                    typeof(CopyTradeRemoveDistributedAccountCommandHandler)
                )
                .AddEvents(
                    typeof(AccountStateUpdatedEvent),
                    typeof(AccountTradeOrderOpenRequestedEvent),
                    typeof(AccountTradeOrderOpenRequestDistributedEvent),
                    typeof(AccountTradeOrderCloseRequestedEvent),
                    typeof(AccountTradeOrderCloseRequestDistributedEvent),
                    typeof(AccountTradeOrderInactivatedEvent),
                    typeof(DistributionGroupPublisherAddedEvent),
                    typeof(DistributionGroupSubscriberAddedEvent),
                    typeof(DistributionGroupPublishedOpenEvent),
                    typeof(DistributionGroupPublishedCloseEvent),
                    typeof(CopyTradeOpenedEvent),
                    typeof(CopyTradeApplicantAddedEvent),
                    typeof(CopyTradeClosedEvent),
                    typeof(CopyTradeApplicantRemovedEvent),
                    typeof(CopyTradeFinishedEvent)
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
                .RegisterServices(sr => {
                    sr.RegisterType(typeof(CopyTradeReadModelLocator));
                })
                .UseInMemoryReadStoreFor<DistributionGroupReadModel>()
                .UseInMemoryReadStoreFor<CopyTradeReadModel, CopyTradeReadModelLocator>()
                .AddQueryHandler<CopyTradeReadModelAllQueryHandler, CopyTradeReadModelAllQuery, IReadOnlyCollection<CopyTradeReadModel>>();
            return options;
        }
    }
}
