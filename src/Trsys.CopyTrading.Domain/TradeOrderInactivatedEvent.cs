using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class AccountTradeOrderInactivatedEvent : AggregateEvent<AccountAggregate, AccountId>
    {
        public AccountTradeOrderInactivatedEvent(TradeOrderId tradeOrderId, CopyTradeId copyTradeId)
        {
            TradeOrderId = tradeOrderId;
            CopyTradeId = copyTradeId;
        }

        public TradeOrderId TradeOrderId { get; }
        public CopyTradeId CopyTradeId { get; }
    }
}