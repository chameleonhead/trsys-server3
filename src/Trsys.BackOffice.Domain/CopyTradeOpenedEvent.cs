using EventFlow.Aggregates;
using Trsys.Core;

namespace Trsys.BackOffice.Domain
{
    public class CopyTradeOpenedEvent : AggregateEvent<CopyTradeAggregate, CopyTradeId>
    {
        public CopyTradeOpenedEvent(DistributionGroupId distributionGroupId, ForexTradeSymbol symbol, OrderType orderType)
        {
            DistributionGroupId = distributionGroupId;
            Symbol = symbol;
            OrderType = orderType;
        }

        public DistributionGroupId DistributionGroupId { get; }
        public ForexTradeSymbol Symbol { get; }
        public OrderType OrderType { get; }
    }
}