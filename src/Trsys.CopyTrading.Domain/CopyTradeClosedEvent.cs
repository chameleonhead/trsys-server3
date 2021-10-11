using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class CopyTradeClosedEvent : AggregateEvent<CopyTradeAggregate, CopyTradeId>
    {
        public CopyTradeClosedEvent(PublisherIdentifier clientKey, AccountId[] tradeApplicants)
        {
            ClientKey = clientKey;
            TradeApplicants = tradeApplicants;
        }

        public PublisherIdentifier ClientKey { get; }
        public AccountId[] TradeApplicants { get; }
    }
}
