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
                    typeof(PublisherRegisterCommand),
                    typeof(PublisherUnregisterCommand),
                    typeof(PublisherUpdateOrderTextCommand),
                    typeof(SubscriberRegisterCommand),
                    typeof(SubscriberUnregisterCommand),
                    typeof(SubscriberOpenTradeOrderCommand),
                    typeof(SubscriberCloseTradeOrderCommand),
                    typeof(SubscriberDistributeOrderTextCommand)
                )
                .AddCommandHandlers(
                    typeof(PublisherRegisterCommandHandler),
                    typeof(PublisherUnregisterCommandHandler),
                    typeof(PublisherUpdateOrderTextCommandHandler),
                    typeof(SubscriberRegisterCommandHandler),
                    typeof(SubscriberEaUnregisterCommandHandler),
                    typeof(SubscriberOpenTradeOrderCommandHandler),
                    typeof(SubscriberCloseTradeOrderCommandHandler),
                    typeof(SubscriberDistributeOrderTextCommandHandler)
                )
                .AddEvents(
                    typeof(PublisherRegisteredEvent),
                    typeof(PublisherUnregisteredEvent),
                    typeof(PublisherOrderTextChangedEvent),
                    typeof(PublisherOpenedOrderEvent),
                    typeof(PublisherClosedOrderEvent),
                    typeof(SubscriberRegisteredEvent),
                    typeof(SubscriberUnregisteredEvent),
                    typeof(SubscriberTradeOrderOpenRequestAppliedEvent),
                    typeof(SubscriberTradeOrderCloseRequestAppliedEvent),
                    typeof(SubscriberOrderTextChangedEvent),
                    typeof(SubscriberDistributedOrderTextChangedEvent)
                )
                .AddSubscribers(
                    typeof(PublisherOrderEventSubscriber),
                    typeof(SubscriberRegistrationEventSubscriber)
                );
            options
                .RegisterServices(sr =>
                {
                    sr.RegisterType(typeof(PublisherReadModelLocator));
                    sr.RegisterType(typeof(SubscriberReadModelLocator));
                })
                .UseInMemoryReadStoreFor<PublisherReadModel, PublisherReadModelLocator>()
                .UseInMemoryReadStoreFor<SubscriberReadModel, SubscriberReadModelLocator>();
            return options;
        }
    }
}
