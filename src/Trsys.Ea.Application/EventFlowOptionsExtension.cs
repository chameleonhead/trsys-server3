using EventFlow;
using EventFlow.Extensions;
using Trsys.Ea.Application.Read.Models;
using Trsys.Ea.Application.Write.Commands;
using Trsys.Ea.Application.Write.Sagas.Ea;
using Trsys.Ea.Application.Write.Subscribers;
using Trsys.Ea.Domain;

namespace Trsys.Ea.Application
{
    public static class EventFlowOptionsExtension
    {
        public static IEventFlowOptions UseEaApplication(this IEventFlowOptions options)
        {
            options
                .AddCommands(
                    typeof(PublisherEaRegisterCommand),
                    typeof(PublisherEaUnregisterCommand),
                    typeof(PublisherEaUpdateOrderTextCommand),
                    typeof(SubscriberEaRegisterCommand),
                    typeof(SubscriberEaUnregisterCommand),
                    typeof(SubscriberEaOpenTradeOrderCommand),
                    typeof(SubscriberEaCloseTradeOrderCommand),
                    typeof(SubscriberEaDistributeOrderTextCommand)
                )
                .AddCommandHandlers(
                    typeof(PublisherEaRegisterCommandHandler),
                    typeof(PublisherEaUnregisterCommandHandler),
                    typeof(PublisherEaUpdateOrderTextCommandHandler),
                    typeof(SubscriberEaRegisterCommandHandler),
                    typeof(SubscriberEaUnregisterCommandHandler),
                    typeof(SubscriberEaOpenTradeOrderCommandHandler),
                    typeof(SubscriberEaCloseTradeOrderCommandHandler),
                    typeof(SubscriberEaDistributeOrderTextCommandHandler)
                )
                .AddEvents(
                    typeof(PublisherEaRegisteredEvent),
                    typeof(PublisherEaUnregisteredEvent),
                    typeof(PublisherEaOrderTextChangedEvent),
                    typeof(PublisherEaOpenedOrderEvent),
                    typeof(PublisherEaClosedOrderEvent),
                    typeof(SubscriberEaRegisteredEvent),
                    typeof(SubscriberEaUnregisteredEvent),
                    typeof(SubscriberEaTradeOrderOpenRequestAppliedEvent),
                    typeof(SubscriberEaTradeOrderCloseRequestAppliedEvent),
                    typeof(SubscriberEaOrderTextChangedEvent),
                    typeof(SubscriberEaDistributedOrderTextChangedEvent)
                )
                .AddSubscribers(
                    typeof(AccountTradeOrderRequestEventSubscriber)
                )
                .AddSagaLocators(
                    typeof(SubscriberEaRegistrationSagaLocator),
                    typeof(OrderPublishingSagaLocator)
                )
                .AddSagas(
                    typeof(SubscriberEaRegistrationSaga),
                    typeof(OrderPublishingSaga)
                );
            options
                .RegisterServices(sr => {
                    sr.RegisterType(typeof(PublisherEaReadModelLocator));
                    sr.RegisterType(typeof(SubscriberEaReadModelLocator));
                    sr.RegisterType(typeof(AccountIdToSubscriberEaIdReadModelLocator));
                })
                .UseInMemoryReadStoreFor<PublisherEaReadModel, PublisherEaReadModelLocator>()
                .UseInMemoryReadStoreFor<SubscriberEaReadModel, SubscriberEaReadModelLocator>()
                .UseInMemoryReadStoreFor<AccountIdToSubscriberEaIdReadModel, AccountIdToSubscriberEaIdReadModelLocator>();
            return options;
        }
    }
}
