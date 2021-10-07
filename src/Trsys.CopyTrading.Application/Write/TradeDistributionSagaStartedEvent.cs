using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Application.Write
{
    public class TradeDistributionSagaStartedEvent: AggregateEvent<TradeDistributionSaga, TradeDistributionSagaId>
    {
    }
}