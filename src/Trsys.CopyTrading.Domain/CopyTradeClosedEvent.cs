using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class CopyTradeClosedEvent : AggregateEvent<CopyTradeAggregate, CopyTradeId>
    {
        public CopyTradeClosedEvent(PublisherId publisherId, AccountId[] tradeApplicants)
        {
            PublisherId = publisherId;
            TradeApplicants = tradeApplicants;
        }

        public PublisherId PublisherId { get; }
        public AccountId[] TradeApplicants { get; }
    }
}
