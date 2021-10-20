using EventFlow;
using EventFlow.Extensions;
using System.Collections.Generic;
using Trsys.CopyTrading.Application.Read.Models;
using Trsys.CopyTrading.Application.Read.Queries;
using Trsys.CopyTrading.Application.Write.Commands;
using Trsys.CopyTrading.Application.Write.Sagas.Ea;
using Trsys.CopyTrading.Application.Write.Sagas.TradeDistribution;
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
                    typeof(AccountRequestOpenTradeOrderCommand),
                    typeof(AccountRequestCloseTradeOrderCommand),
                    typeof(AccountDistributeRequestOpenTradeOrderCommand),
                    typeof(AccountDistributeRequestCloseTradeOrderCommand),
                    typeof(AddPublisherCommand),
                    typeof(AddSubscriberCommand),
                    typeof(PublishOrderOpenCommand),
                    typeof(OpenCopyTradeCommand),
                    typeof(CopyTradeAddDistributedAccountCommand),
                    typeof(PublishOrderCloseCommand),
                    typeof(CloseCopyTradeCommand),
                    typeof(RegisterPublisherEaCommand),
                    typeof(PublisherEaUpdateOrdersCommand),
                    typeof(RegisterSubscriberEaCommand),
                    typeof(SubscriberEaUpdateOrdersCommand)
                )
                .AddCommandHandlers(
                    typeof(AccountStateUpdateCommandHandler),
                    typeof(AccountRequestOpenTradeOrderCommandHandler),
                    typeof(AccountRequestCloseTradeOrderCommandHandler),
                    typeof(AccountDistributeRequestOpenTradeOrderCommandHandler),
                    typeof(AccountDistributeRequestCloseTradeOrderCommandHandler),
                    typeof(AddPublisherCommandHandler),
                    typeof(AddSubscriberCommandHandler),
                    typeof(PublishOrderOpenCommandHandler),
                    typeof(OpenCopyTradeCommandHandler),
                    typeof(CopyTradeAddDistributedAccountCommandHandler),
                    typeof(PublishOrderCloseCommandHandler),
                    typeof(CloseCopyTradeCommandHandler),
                    typeof(RegisterPublisherSecretKeyCommandHandler),
                    typeof(PublisherEaUpdateOrdersCommandHandler),
                    typeof(RegisterSubscriberEaCommandHandler),
                    typeof(SubscriberEaUpdateOrdersCommandHandler)
                )
                .AddEvents(
                    typeof(AccountStateUpdatedEvent),
                    typeof(AccountTradeOrderOpenRequestedEvent),
                    typeof(AccountTradeOrderCloseRequestedEvent),
                    typeof(PublisherAddedEvent),
                    typeof(SubscriberAddedEvent),
                    typeof(CopyTradeOpenedEvent),
                    typeof(TradeOpenDistributionStartedEvent),
                    typeof(CopyTradeApplicantAddedEvent),
                    typeof(AccountTradeOrderOpenRequestDistributedEvent),
                    typeof(TradeCloseDistributionStartedEvent),
                    typeof(CopyTradeClosedEvent),
                    typeof(AccountTradeOrderCloseRequestDistributedEvent),
                    typeof(AccountTradeOrderInactivatedEvent),
                    typeof(PublisherEaRegisteredEvent),
                    typeof(PublisherEaOrderTextUpdatedEvent),
                    typeof(SubscriberEaRegisteredEvent),
                    typeof(SubscriberEaOrderTextUpdatedEvent),
                    typeof(PublisherEaOpenedOrderEvent),
                    typeof(PublisherEaClosedOrderEvent)
                )
                .AddSagaLocators(
                    typeof(TradeDistributionSagaLocator),
                    typeof(PublisherEaRegistrationSagaLocator),
                    typeof(SubscriberEaRegistrationSagaLocator),
                    typeof(OrderPublishingSagaLocator)
                )
                .AddSagas(
                    typeof(TradeDistributionSaga),
                    typeof(PublisherEaRegistrationSaga),
                    typeof(SubscriberEaRegistrationSaga),
                    typeof(OrderPublishingSaga)
                )
                .AddEvents(
                    typeof(TradeDistributionSagaStartedEvent),
                    typeof(TradeDistributionSagaFinishedEvent)
                );
            options
                .RegisterServices(sr => {
                    sr.RegisterType(typeof(CopyTradeReadModelLocator));
                    sr.RegisterType(typeof(PublisherEaReadModelLocator));
                    sr.RegisterType(typeof(SubscriberEaReadModelLocator));
                })
                .UseInMemoryReadStoreFor<AccountReadModel>()
                .UseInMemoryReadStoreFor<DistributionGroupReadModel>()
                .UseInMemoryReadStoreFor<CopyTradeReadModel, CopyTradeReadModelLocator>()
                .UseInMemoryReadStoreFor<PublisherEaReadModel, PublisherEaReadModelLocator>()
                .UseInMemoryReadStoreFor<SubscriberEaReadModel, SubscriberEaReadModelLocator>()
                .AddQueryHandler<CopyTradeReadModelAllQueryHandler, CopyTradeReadModelAllQuery, IReadOnlyCollection<CopyTradeReadModel>>();
            return options;
        }
    }
}
