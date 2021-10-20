using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class AccountTradeOrderCloseRequestDistributedEvent : AggregateEvent<AccountAggregate, AccountId>
    {
        public AccountTradeOrderCloseRequestDistributedEvent(TradeOrderId tradeOrderId, CopyTradeId copyTradeId)
        {
            TradeOrderId = tradeOrderId;
            CopyTradeId = copyTradeId;
        }

        public TradeOrderId TradeOrderId { get; }
        public CopyTradeId CopyTradeId { get; }
    }
}