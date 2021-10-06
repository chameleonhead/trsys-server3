using EventFlow.Aggregates;
using EventFlow.Sagas;
using EventFlow.Sagas.AggregateSagas;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write
{
    public class TradeDistributionSaga :
        AggregateSaga<TradeDistributionSaga, TradeDistributionId, TradeDistributionSagaLocator>,
        ISagaIsStartedBy<CopyTradeAggregate, CopyTradeId, CopyTradeOpenedEvent>
    {
        public TradeDistributionSaga(TradeDistributionId id) : base(id)
        {
        }

        public Task HandleAsync(IDomainEvent<CopyTradeAggregate, CopyTradeId, CopyTradeOpenedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            var aggregateEvent = domainEvent.AggregateEvent;
            Publish(new StartTradeDistributionCommand(
                aggregateEvent.DistributionGroupId, 
                domainEvent.AggregateIdentity,
                aggregateEvent.Symbol, 
                aggregateEvent.OrderType));
            return Task.CompletedTask;
        }
    }
}
