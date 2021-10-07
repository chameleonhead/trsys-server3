using EventFlow.Aggregates;
using System;
using System.Collections.Generic;

namespace Trsys.CopyTrading.Domain
{
    public class AccountAggregate : AggregateRoot<AccountAggregate, AccountId>
    {
        private readonly Dictionary<CopyTradeId, TradeOrderEntity> ActiveTrades = new();

        public AccountAggregate(AccountId id) : base(id)
        {
        }

        public void OpenTrade(CopyTradeId copyTradeId, ForexTradeSymbol symbol, OrderType orderType, TradeQuantity quantity)
        {
            if (!ActiveTrades.ContainsKey(copyTradeId))
            {
                Emit(new TradeOrderOpenedEvent(TradeOrderId.New, copyTradeId, symbol, orderType, quantity), new Metadata(KeyValuePair.Create("copy-trade-id", copyTradeId.Value)));
            }
        }

        public void CloseTrade(CopyTradeId copyTradeId)
        {
            if (!ActiveTrades.TryGetValue(copyTradeId, out var entity))
            {
                throw new InvalidOperationException();
            }
            Emit(new TradeOrderClosedEvent(entity.Id, entity.CopyTradeId));
        }

        public void Apply(TradeOrderOpenedEvent e)
        {
            ActiveTrades.Add(e.CopyTradeId, new TradeOrderEntity(e.TradeOrderId, e.CopyTradeId, e.Symbol, e.OrderType, e.Quantity));
        }

        public void Apply(TradeOrderClosedEvent e)
        {
            ActiveTrades.Remove(e.CopyTradeId);
        }
    }
}
