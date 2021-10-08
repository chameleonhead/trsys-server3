using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Application.Write.Sagas
{
    public class TradeDistributionSagaStartedEvent: AggregateEvent<TradeDistributionSaga, TradeDistributionSagaId>
    {
    }
}