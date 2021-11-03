using EventFlow;
using EventFlow.Extensions;
using Trsys.Frontend.Application.Read.Models;
using Trsys.Frontend.Application.Write.Commands;
using Trsys.Frontend.Application.Write.Subscribers;
using Trsys.Frontend.Domain;

namespace Trsys.Frontend.Application
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
                    typeof(PublisherEaOrderEventSubscriber),
                    typeof(SubscriberEaRegistrationEventSubscriber)
                );
            options
                .RegisterServices(sr => {
                    sr.RegisterType(typeof(PublisherEaReadModelLocator));
                    sr.RegisterType(typeof(SubscriberEaReadModelLocator));
                    sr.RegisterType(typeof(SubscriberIdToSubscriberEaIdReadModelLocator));
                })
                .UseInMemoryReadStoreFor<PublisherEaReadModel, PublisherEaReadModelLocator>()
                .UseInMemoryReadStoreFor<SubscriberEaReadModel, SubscriberEaReadModelLocator>()
                .UseInMemoryReadStoreFor<SubscriberIdToSubscriberEaIdReadModel, SubscriberIdToSubscriberEaIdReadModelLocator>();
            return options;
        }
    }
}
