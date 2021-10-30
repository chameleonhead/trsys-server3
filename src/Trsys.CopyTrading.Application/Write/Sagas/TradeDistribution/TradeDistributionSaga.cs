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
        ISagaHandles<DistributionGroupAggregate, DistributionGroupId, DistributionGroupClosePublishedEvent>,
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

        public Task HandleAsync(IDomainEvent<CopyTradeAggregate, CopyTradeId, CopyTradeDistributedSubscriberAddedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            Publish(new CopyTradeAddDistributedSubscriberCommand(domainEvent.AggregateIdentity, domainEvent.AggregateEvent.SubscriberId));
            return Task.CompletedTask;
        }

        public Task HandleAsync(IDomainEvent<DistributionGroupAggregate, DistributionGroupId, DistributionGroupClosePublishedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            var aggregateEvent = domainEvent.AggregateEvent;
            Publish(new CopyTradeCloseCommand(aggregateEvent.CopyTradeId, domainEvent.AggregateIdentity, aggregateEvent.PublisherId));
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
