using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;

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
            Emit(new TradeOrderOpenedEvent(TradeOrderId.New, copyTradeId, symbol, orderType, quantity), new Metadata(KeyValuePair.Create("copy-trade-id", copyTradeId.Value)));
        }

        public void Apply(TradeOrderOpenedEvent e)
        {
            ActiveTrades.Add(new TradeOrderEntity(e.TradeOrderId, e.CopyTradeId, e.Symbol, e.OrderType, e.Quantity));
        }
    }
}
