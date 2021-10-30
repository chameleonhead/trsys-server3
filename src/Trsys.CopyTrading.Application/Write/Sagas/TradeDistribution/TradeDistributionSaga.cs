using EventFlow.Aggregates;
using EventFlow.Sagas;
using EventFlow.Sagas.AggregateSagas;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Application.Write.Commands;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Sagas.TradeDistribution
{
    public class TradeDistributionSaga :
        AggregateSaga<TradeDistributionSaga, TradeDistributionSagaId, TradeDistributionSagaLocator>,
        IEmit<TradeDistributionSagaStartedEvent>,
        IEmit<TradeDistributionSagaFinishedEvent>,
        ISagaIsStartedBy<DistributionGroupAggregate, DistributionGroupId, DistributionGroupOpenPublishedEvent>,
        ISagaHandles<CopyTradeAggregate, CopyTradeId, CopyTradeOpenedEvent>,
        ISagaHandles<AccountAggregate, AccountId, AccountTradeOrderOpenRequestDistributedEvent>,
        ISagaHandles<DistributionGroupAggregate, DistributionGroupId, DistributionGroupClosePublishedEvent>,
        ISagaHandles<CopyTradeAggregate, CopyTradeId, CopyTradeClosedEvent>,
        ISagaHandles<AccountAggregate, AccountId, AccountTradeOrderInactivatedEvent>,
        ISagaHandles<CopyTradeAggregate, CopyTradeId, CopyTradeFinishedEvent>
    {
        public TradeDistributionSaga(TradeDistributionSagaId id) : base(id)
        {
        }

        public Task HandleAsync(IDomainEvent<DistributionGroupAggregate, DistributionGroupId, DistributionGroupOpenPublishedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            var aggregateEvent = domainEvent.AggregateEvent;

            Emit(new TradeDistributionSagaStartedEvent(aggregateEvent.CopyTradeId));

            Publish(new CopyTradeOpenCommand(
                aggregateEvent.CopyTradeId,
                domainEvent.AggregateIdentity,
                aggregateEvent.PublisherId,
                aggregateEvent.Symbol,
                aggregateEvent.OrderType,
                aggregateEvent.Subscribers));
            return Task.CompletedTask;
        }

        public Task HandleAsync(IDomainEvent<CopyTradeAggregate, CopyTradeId, CopyTradeOpenedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            var aggregateEvent = domainEvent.AggregateEvent;
            foreach (var accountId in aggregateEvent.Subscribers)
            {
                Publish(new AccountRequestOpenTradeOrderCommand(
                    accountId,
                    domainEvent.AggregateIdentity,
                    aggregateEvent.DistributionGroupId,
                    aggregateEvent.Symbol,
                    aggregateEvent.OrderType));
            }
            return Task.CompletedTask;
        }

        public Task HandleAsync(IDomainEvent<AccountAggregate, AccountId, AccountTradeOrderOpenRequestDistributedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            var aggregateEvent = domainEvent.AggregateEvent;
            Publish(new CopyTradeAddDistributedAccountCommand(aggregateEvent.CopyTradeId, domainEvent.AggregateIdentity));
            return Task.CompletedTask;
        }

        public Task HandleAsync(IDomainEvent<DistributionGroupAggregate, DistributionGroupId, DistributionGroupClosePublishedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            var aggregateEvent = domainEvent.AggregateEvent;
            Publish(new CopyTradeCloseCommand(aggregateEvent.CopyTradeId, domainEvent.AggregateIdentity, aggregateEvent.PublisherId));
            return Task.CompletedTask;
        }

        public Task HandleAsync(IDomainEvent<CopyTradeAggregate, CopyTradeId, CopyTradeClosedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            var aggregateEvent = domainEvent.AggregateEvent;
            foreach (var accountId in aggregateEvent.Subscribers)
            {
                Publish(new AccountRequestCloseTradeOrderCommand(accountId, domainEvent.AggregateIdentity));
            }
            return Task.CompletedTask;
        }

        public Task HandleAsync(IDomainEvent<AccountAggregate, AccountId, AccountTradeOrderInactivatedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            var aggregateEvent = domainEvent.AggregateEvent;
            Publish(new CopyTradeRemoveDistributedAccountCommand(aggregateEvent.CopyTradeId, domainEvent.AggregateIdentity));
            return Task.CompletedTask;
        }

        public Task HandleAsync(IDomainEvent<CopyTradeAggregate, CopyTradeId, CopyTradeFinishedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            Emit(new TradeDistributionSagaFinishedEvent());
            return Task.CompletedTask;
        }

        public void Apply(TradeDistributionSagaStartedEvent e)
        {
        }

        public void Apply(TradeDistributionSagaFinishedEvent e)
        {
            Complete();
        }
    }
}
