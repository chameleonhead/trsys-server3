using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class CopyTradeClosedEvent : AggregateEvent<CopyTradeAggregate, CopyTradeId>
    {
        public CopyTradeClosedEvent(AccountId[] tradeApplicants)
        {
            TradeApplicants = tradeApplicants;
        }

        public AccountId[] TradeApplicants { get; }
    }
}
