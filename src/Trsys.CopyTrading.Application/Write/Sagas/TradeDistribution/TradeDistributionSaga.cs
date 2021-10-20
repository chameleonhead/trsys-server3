using EventFlow.Aggregates;
using EventFlow.Sagas;
using EventFlow.Sagas.AggregateSagas;
using System.Linq;
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
        ISagaIsStartedBy<DistributionGroupAggregate, DistributionGroupId, TradeOpenDistributionStartedEvent>,
        ISagaHandles<AccountAggregate, AccountId, AccountTradeOrderOpenRequestDistributedEvent>,
        ISagaHandles<DistributionGroupAggregate, DistributionGroupId, TradeCloseDistributionStartedEvent>,
        ISagaHandles<CopyTradeAggregate, CopyTradeId, CopyTradeClosedEvent>,
        ISagaHandles<AccountAggregate, AccountId, AccountTradeOrderInactivatedEvent>
    {
        public TradeDistributionSaga(TradeDistributionSagaId id) : base(id)
        {
        }

        public Task HandleAsync(IDomainEvent<DistributionGroupAggregate, DistributionGroupId, TradeOpenDistributionStartedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            var aggregateEvent = domainEvent.AggregateEvent;

            Emit(new TradeDistributionSagaStartedEvent(aggregateEvent.CopyTradeId));

            Publish(new OpenCopyTradeCommand(
                aggregateEvent.CopyTradeId,
                domainEvent.AggregateIdentity,
                aggregateEvent.PublisherId,
                aggregateEvent.Sequence,
                aggregateEvent.Symbol,
                aggregateEvent.OrderType,
                aggregateEvent.Subscribers));
            foreach (var accountId in aggregateEvent.Subscribers)
            {
                Publish(new AccountRequestOpenTradeOrderCommand(
                    accountId,
                    aggregateEvent.CopyTradeId,
                    domainEvent.AggregateIdentity,
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

        public Task HandleAsync(IDomainEvent<DistributionGroupAggregate, DistributionGroupId, TradeCloseDistributionStartedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            var aggregateEvent = domainEvent.AggregateEvent;
            Publish(new CloseCopyTradeCommand(aggregateEvent.CopyTradeId, domainEvent.AggregateIdentity, aggregateEvent.PublisherId));
            return Task.CompletedTask;
        }

        public Task HandleAsync(IDomainEvent<CopyTradeAggregate, CopyTradeId, CopyTradeClosedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            var aggregateEvent = domainEvent.AggregateEvent;
            if (!aggregateEvent.TradeApplicants.Any())
            {
                Emit(new TradeDistributionSagaFinishedEvent());
            }
            return Task.CompletedTask;
        }

        public Task HandleAsync(IDomainEvent<AccountAggregate, AccountId, AccountTradeOrderInactivatedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
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
