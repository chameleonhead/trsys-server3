using EventFlow.Aggregates;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Write.Sagas.TradeDistribution
{
    public class TradeDistributionSagaStartedEvent : AggregateEvent<TradeDistributionSaga, TradeDistributionSagaId>
    {
        public TradeDistributionSagaStartedEvent(CopyTradeId copyTradeId)
        {
            CopyTradeId = copyTradeId;
        }

        public CopyTradeId CopyTradeId { get; }
    }
}