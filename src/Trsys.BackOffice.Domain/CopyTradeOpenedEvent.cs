using EventFlow.Aggregates;

namespace Trsys.BackOffice.Domain
{
    public class CopyTradeOpenedEvent : AggregateEvent<CopyTradeAggregate, CopyTradeId>
    {
        public CopyTradeOpenedEvent(DistributionGroupId distributionGroupId, CopyTradeSymbol symbol, CopyTradeOrderType orderType)
        {
            DistributionGroupId = distributionGroupId;
            Symbol = symbol;
            OrderType = orderType;
        }

        public DistributionGroupId DistributionGroupId { get; }
        public CopyTradeSymbol Symbol { get; }
        public CopyTradeOrderType OrderType { get; }
    }
}