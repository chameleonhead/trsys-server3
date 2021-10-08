using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Application.Write.Sagas
{
    public class TradeDistributionSagaFinishedEvent : AggregateEvent<TradeDistributionSaga, TradeDistributionSagaId>
    {
    }
}