using EventFlow.Aggregates;
using EventFlow.Sagas;
using EventFlow.Sagas.AggregateSagas;
using System.Threading;
using System.Threading.Tasks;
using Trsys.Core;
using Trsys.CopyTrading.Application.Write.Commands;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Sagas.TradeDistribution
{
    public class TradeDistributionSaga :
        AggregateSaga<TradeDistributionSaga, TradeDistributionSagaId, TradeDistributionSagaLocator>,
        IEmit<TradeDistributionSagaStartedEvent>,
        IEmit<TradeDistributionSagaFinishedEvent>,
        ISagaIsStartedBy<DistributionGroupAggregate, DistributionGroupId, DistributionGroupOpenPublishedEvent>,
        ISagaHandles<DistributionGroupAggregate, DistributionGroupId, DistributionGroupClosePublishedEvent>
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
                aggregateEvent.Symbol,
                aggregateEvent.OrderType,
                aggregateEvent.Subscribers));
            return Task.CompletedTask;
        }

        public Task HandleAsync(IDomainEvent<DistributionGroupAggregate, DistributionGroupId, DistributionGroupClosePublishedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            var aggregateEvent = domainEvent.AggregateEvent;
            Publish(new CopyTradeCloseCommand(aggregateEvent.CopyTradeId, domainEvent.AggregateIdentity));

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
