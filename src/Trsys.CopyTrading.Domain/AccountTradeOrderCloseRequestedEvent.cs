using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class AccountTradeOrderCloseRequestedEvent : AggregateEvent<AccountAggregate, AccountId>
    {
        public AccountTradeOrderCloseRequestedEvent(TradeOrderId tradeOrderId, CopyTradeId copyTradeId)
        {
            TradeOrderId = tradeOrderId;
            CopyTradeId = copyTradeId;
        }

        public TradeOrderId TradeOrderId { get; }
        public CopyTradeId CopyTradeId { get; }
    }
}
