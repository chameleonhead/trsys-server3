using EventFlow;
using EventFlow.Aggregates;
using EventFlow.Queries;
using EventFlow.Subscribers;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;
using Trsys.Ea.Application.Read.Models;
using Trsys.Ea.Application.Write.Commands;
using Trsys.Ea.Domain;

namespace Trsys.Ea.Application.Write.Subscribers
{
    public class AccountTradeOrderRequestEventSubscriber :
        ISubscribeSynchronousTo<AccountAggregate, AccountId, AccountTradeOrderOpenRequestedEvent>,
        ISubscribeSynchronousTo<AccountAggregate, AccountId, AccountTradeOrderCloseRequestedEvent>
    {
        private readonly ICommandBus commandBus;
        private readonly IQueryProcessor queryProcessor;

        public AccountTradeOrderRequestEventSubscriber(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            this.commandBus = commandBus;
            this.queryProcessor = queryProcessor;
        }

        public async Task HandleAsync(IDomainEvent<AccountAggregate, AccountId, AccountTradeOrderOpenRequestedEvent> domainEvent, CancellationToken cancellationToken)
        {
            var subscriber = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<AccountIdToSubscriberEaIdReadModel>(domainEvent.AggregateIdentity.Value), cancellationToken);
            if (subscriber == null)
            {
                return;
            }
            await commandBus.PublishAsync(new SubscriberEaOpenTradeOrderCommand(SubscriberEaId.With(subscriber.SubscriberEaId), domainEvent.AggregateEvent.CopyTradeId, domainEvent.AggregateEvent.Symbol, domainEvent.AggregateEvent.OrderType), cancellationToken);
        }

        public async Task HandleAsync(IDomainEvent<AccountAggregate, AccountId, AccountTradeOrderCloseRequestedEvent> domainEvent, CancellationToken cancellationToken)
        {
            var subscriber = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<AccountIdToSubscriberEaIdReadModel>(domainEvent.AggregateIdentity.Value), cancellationToken);
            if (subscriber == null)
            {
                return;
            }
            await commandBus.PublishAsync(new SubscriberEaCloseTradeOrderCommand(SubscriberEaId.With(subscriber.SubscriberEaId), domainEvent.AggregateEvent.CopyTradeId), cancellationToken);
        }
    }
}
