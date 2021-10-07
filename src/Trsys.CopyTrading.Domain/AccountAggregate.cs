using EventFlow.Aggregates;
using System;
using System.Collections.Generic;

namespace Trsys.CopyTrading.Domain
{
    public class AccountAggregate : AggregateRoot<AccountAggregate, AccountId>,
        IEmit<AccountStateUpdatedEvent>,
        IEmit<TradeOrderOpenedEvent>,
        IEmit<TradeOrderOpenDistributedEvent>,
        IEmit<TradeOrderClosedEvent>,
        IEmit<TradeOrderCloseDistributedEvent>
    {
        private readonly Dictionary<CopyTradeId, TradeOrderEntity> ActiveTrades = new();

        public AccountAggregate(AccountId id) : base(id)
        {
        }

        public void UpdateState(AccountBalance balance)
        {
            Emit(new AccountStateUpdatedEvent(balance));
        }

        public void OpenTrade(CopyTradeId copyTradeId, ForexTradeSymbol symbol, OrderType orderType, TradeQuantity quantity)
        {
            if (!ActiveTrades.ContainsKey(copyTradeId))
            {
                Emit(new TradeOrderOpenedEvent(TradeOrderId.New, copyTradeId, symbol, orderType, quantity), new Metadata(KeyValuePair.Create("copy-trade-id", copyTradeId.Value)));
            }
        }

        public void OpenTradeDistributed(CopyTradeId copyTradeId)
        {
            if (ActiveTrades.TryGetValue(copyTradeId, out var entity))
            {
                Emit(new TradeOrderOpenDistributedEvent(entity.Id, entity.CopyTradeId));
            }
        }

        public void CloseTrade(CopyTradeId copyTradeId)
        {
            if (!ActiveTrades.TryGetValue(copyTradeId, out var entity))
            {
                throw new InvalidOperationException();
            }
            Emit(new TradeOrderClosedEvent(entity.Id, entity.CopyTradeId));
            if (!entity.IsOpenDistributed)
            {
                Emit(new TradeOrderInactivatedEvent(entity.Id, entity.CopyTradeId));
            }
        }

        public void CloseTradeDistributed(CopyTradeId copyTradeId)
        {
            if (ActiveTrades.TryGetValue(copyTradeId, out var entity))
            {
                Emit(new TradeOrderCloseDistributedEvent(entity.Id, entity.CopyTradeId));
            }
        }

        public void Apply(AccountStateUpdatedEvent e)
        {
        }

        public void Apply(TradeOrderOpenedEvent e)
        {
            ActiveTrades.Add(e.CopyTradeId, new TradeOrderEntity(e.TradeOrderId, e.CopyTradeId, e.Symbol, e.OrderType, e.Quantity));
        }

        public void Apply(TradeOrderOpenDistributedEvent e)
        {
            ActiveTrades[e.CopyTradeId].OpenDistributed();
        }

        public void Apply(TradeOrderClosedEvent e)
        {
            ActiveTrades[e.CopyTradeId].CloseDistributed();
        }

        public void Apply(TradeOrderCloseDistributedEvent e)
        {
            ActiveTrades[e.CopyTradeId].CloseDistributed();
        }

        public void Apply(TradeOrderInactivatedEvent e)
        {
            ActiveTrades.Remove(e.CopyTradeId);
        }
    }
}
