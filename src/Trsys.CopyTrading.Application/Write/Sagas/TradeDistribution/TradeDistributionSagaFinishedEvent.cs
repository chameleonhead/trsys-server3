using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Application.Write.Sagas.TradeDistribution
{
    public class TradeDistributionSagaFinishedEvent : AggregateEvent<TradeDistributionSaga, TradeDistributionSagaId>
    {
    }
}