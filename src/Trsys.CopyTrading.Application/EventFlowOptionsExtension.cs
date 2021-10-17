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
                    typeof(AddPublisherCommand),
                    typeof(AddSubscriberCommand),
                    typeof(PublishOrderOpenCommand),
                    typeof(OpenCopyTradeCommand),
                    typeof(AddCopyTradeDistributedAccountCommand),
                    typeof(DistributeOpenTradeCommand),
                    typeof(PublishOrderCloseCommand),
                    typeof(CloseCopyTradeCommand),
                    typeof(DistributeCloseTradeCommand),
                    typeof(RegisterPublisherEaCommand),
                    typeof(PublisherEaUpdateOrdersCommand),
                    typeof(RegisterSubscriberEaCommand),
                    typeof(SubscriberEaUpdateOrdersCommand)
                )
                .AddCommandHandlers(
                    typeof(AccountStateUpdateCommandHandler),
                    typeof(AddPublisherCommandHandler),
                    typeof(AddSubscriberCommandHandler),
                    typeof(PublishOrderOpenCommandHandler),
                    typeof(OpenCopyTradeCommandHandler),
                    typeof(AddCopyTradeDistributedAccountCommandHandler),
                    typeof(DistributeOpenTradeCommandHandler),
                    typeof(PublishOrderCloseCommandHandler),
                    typeof(CloseCopyTradeCommandHandler),
                    typeof(DistributeCloseTradeCommandHandler),
                    typeof(RegisterPublisherSecretKeyCommandHandler),
                    typeof(PublisherEaUpdateOrdersCommandHandler),
                    typeof(RegisterSubscriberEaCommandHandler),
                    typeof(SubscriberEaUpdateOrdersCommandHandler)
                )
                .AddEvents(
                    typeof(AccountStateUpdatedEvent),
                    typeof(PublisherAddedEvent),
                    typeof(SubscriberAddedEvent),
                    typeof(CopyTradeOpenedEvent),
                    typeof(TradeOpenDistributionStartedEvent),
                    typeof(CopyTradeApplicantAddedEvent),
                    typeof(TradeOrderOpenDistributedEvent),
                    typeof(TradeCloseDistributionStartedEvent),
                    typeof(CopyTradeClosedEvent),
                    typeof(TradeOrderCloseDistributedEvent),
                    typeof(TradeOrderInactivatedEvent),
                    typeof(PublisherEaRegisteredEvent),
                    typeof(PublisherEaOrderTextUpdatedEvent),
                    typeof(SubscriberEaRegisteredEvent),
                    typeof(SubscriberEaOrderTextUpdatedEvent)
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
