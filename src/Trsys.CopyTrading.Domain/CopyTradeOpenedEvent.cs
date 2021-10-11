using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class CopyTradeOpenedEvent : AggregateEvent<CopyTradeAggregate, CopyTradeId>
    {
        public CopyTradeOpenedEvent(PublisherIdentifier publisherId, DistributionGroupId distributionGroupId, ForexTradeSymbol symbol, OrderType orderType)
        {
            PublisherId = publisherId;
            DistributionGroupId = distributionGroupId;
            Symbol = symbol;
            OrderType = orderType;
        }

        public PublisherIdentifier PublisherId { get; }
        public DistributionGroupId DistributionGroupId { get; }
        public ForexTradeSymbol Symbol { get; }
        public OrderType OrderType { get; }
    }
}