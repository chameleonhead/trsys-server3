using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Application.Write
{
    public class TradeDistributionSagaFinishedEvent : AggregateEvent<TradeDistributionSaga, TradeDistributionSagaId>
    {
    }
}