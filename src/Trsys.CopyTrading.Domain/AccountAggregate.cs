using EventFlow.Aggregates;
using System.Collections.Generic;

namespace Trsys.CopyTrading.Domain
{
    public class AccountAggregate : AggregateRoot<AccountAggregate, AccountId>,
        IEmit<AccountStateUpdatedEvent>,
        IEmit<AccountTradeOrderOpenRequestedEvent>,
        IEmit<AccountTradeOrderOpenRequestDistributedEvent>,
        IEmit<AccountTradeOrderCloseRequestedEvent>,
        IEmit<AccountTradeOrderCloseRequestDistributedEvent>,
        IEmit<AccountTradeOrderInactivatedEvent>
    {
        private readonly Dictionary<CopyTradeId, TradeOrderEntity> ActiveTrades = new();

        public AccountAggregate(AccountId id) : base(id)
        {
        }

        public void UpdateState(AccountBalance balance)
        {
            Emit(new AccountStateUpdatedEvent(balance));
        }

        public void RequestOpenTradeOrder(CopyTradeId copyTradeId, ForexTradeSymbol symbol, OrderType orderType)
        {
            if (ActiveTrades.ContainsKey(copyTradeId))
            {
                return;
            }
            Emit(new AccountTradeOrderOpenRequestedEvent(TradeOrderId.New, copyTradeId, symbol, orderType), new Metadata(KeyValuePair.Create("copy-trade-id", copyTradeId.Value)));
        }

        public void RequestCloseTradeOrder(CopyTradeId copyTradeId)
        {
            if (!ActiveTrades.TryGetValue(copyTradeId, out var entity))
            {
                return;
            }
            Emit(new AccountTradeOrderCloseRequestedEvent(entity.Id, copyTradeId));
            if (!entity.HasOpened || entity.HasClosed)
            {
                Emit(new AccountTradeOrderInactivatedEvent(entity.Id, entity.CopyTradeId), new Metadata(KeyValuePair.Create("copy-trade-id", copyTradeId.Value)));
            }
        }

        public void DistributeRequestOpenTradeOrder(CopyTradeId copyTradeId)
        {
            if (!ActiveTrades.TryGetValue(copyTradeId, out var entity))
            {
                return;
            }
            Emit(new AccountTradeOrderOpenRequestDistributedEvent(entity.Id, copyTradeId), new Metadata(KeyValuePair.Create("copy-trade-id", copyTradeId.Value)));
        }

        public void DistributeRequestCloseTradeOrder(CopyTradeId copyTradeId)
        {
            if (!ActiveTrades.TryGetValue(copyTradeId, out var entity))
            {
                return;
            }
            Emit(new AccountTradeOrderCloseRequestDistributedEvent(entity.Id, entity.CopyTradeId), new Metadata(KeyValuePair.Create("copy-trade-id", copyTradeId.Value)));
            Emit(new AccountTradeOrderInactivatedEvent(entity.Id, entity.CopyTradeId), new Metadata(KeyValuePair.Create("copy-trade-id", copyTradeId.Value)));
        }

        public void Apply(AccountStateUpdatedEvent e)
        {
        }

        public void Apply(AccountTradeOrderOpenRequestedEvent e)
        {
            ActiveTrades.Add(e.CopyTradeId, new TradeOrderEntity(e.TradeOrderId, e.CopyTradeId, e.Symbol, e.OrderType));
        }

        public void Apply(AccountTradeOrderOpenRequestDistributedEvent e)
        {
            ActiveTrades[e.CopyTradeId].OpenDistributed();
        }

        public void Apply(AccountTradeOrderCloseRequestedEvent e)
        {
            ActiveTrades.Remove(e.CopyTradeId);
        }

        public void Apply(AccountTradeOrderCloseRequestDistributedEvent e)
        {
            ActiveTrades[e.CopyTradeId].CloseDistributed();
        }

        public void Apply(AccountTradeOrderInactivatedEvent e)
        {
            ActiveTrades.Remove(e.CopyTradeId);
        }
    }
}
