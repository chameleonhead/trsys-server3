using EventFlow.Aggregates;
using System.Collections.Generic;

namespace Trsys.CopyTrading.Domain
{
    public class AccountAggregate : AggregateRoot<AccountAggregate, AccountId>
    {
        private readonly HashSet<TradeOrderEntity> ActiveTrades = new();

        public AccountAggregate(AccountId id) : base(id)
        {
        }

        public void OpenTrade(CopyTradeId copyTradeId, ForexTradeSymbol symbol, OrderType orderType, TradeQuantity quantity)
        {
            Emit(new TradeOrderOpenedEvent(TradeOrderId.New, copyTradeId, symbol, orderType, quantity));
        }

        public void Apply(TradeOrderOpenedEvent e)
        {
            ActiveTrades.Add(new TradeOrderEntity(e.TradeOrderId, e.CopyTradeId, e.Symbol, e.OrderType, e.Quantity));
        }
    }
}
