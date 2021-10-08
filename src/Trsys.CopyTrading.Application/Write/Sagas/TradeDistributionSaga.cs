using EventFlow.Aggregates;
using EventFlow.Sagas;
using EventFlow.Sagas.AggregateSagas;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Application.Write.Commands;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Sagas
{
    public class TradeDistributionSaga :
        AggregateSaga<TradeDistributionSaga, TradeDistributionSagaId, TradeDistributionSagaLocator>,
        IEmit<TradeDistributionSagaStartedEvent>,
        IEmit<TradeDistributionSagaFinishedEvent>,
        ISagaIsStartedBy<CopyTradeAggregate, CopyTradeId, CopyTradeOpenedEvent>,
        ISagaHandles<AccountAggregate, AccountId, TradeOrderOpenDistributedEvent>,
        ISagaHandles<CopyTradeAggregate, CopyTradeId, CopyTradeClosedEvent>,
        ISagaHandles<AccountAggregate, AccountId, TradeOrderCloseDistributedEvent>
    {
        public TradeDistributionSaga(TradeDistributionSagaId id) : base(id)
        {
        }

        public Task HandleAsync(IDomainEvent<CopyTradeAggregate, CopyTradeId, CopyTradeOpenedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            Emit(new TradeDistributionSagaStartedEvent());

            var aggregateEvent = domainEvent.AggregateEvent;
            Publish(new StartTradeDistributionCommand(
                aggregateEvent.DistributionGroupId,
                domainEvent.AggregateIdentity,
                aggregateEvent.Symbol,
                aggregateEvent.OrderType));
            return Task.CompletedTask;
        }

        public Task HandleAsync(IDomainEvent<AccountAggregate, AccountId, TradeOrderOpenDistributedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            var aggregateEvent = domainEvent.AggregateEvent;
            Publish(new AddCopyTradeDistributedAccountCommand(aggregateEvent.CopyTradeId, domainEvent.AggregateIdentity));
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

        public Task HandleAsync(IDomainEvent<AccountAggregate, AccountId, TradeOrderCloseDistributedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
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
