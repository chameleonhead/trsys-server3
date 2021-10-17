using EventFlow.Aggregates;
using System.Collections.Generic;

namespace Trsys.CopyTrading.Domain
{
    public class AccountAggregate : AggregateRoot<AccountAggregate, AccountId>,
        IEmit<AccountStateUpdatedEvent>,
        IEmit<TradeOrderOpenDistributedEvent>,
        IEmit<TradeOrderCloseDistributedEvent>,
        IEmit<TradeOrderInactivatedEvent>
    {
        private readonly Dictionary<CopyTradeId, TradeOrderEntity> ActiveTrades = new();

        public AccountAggregate(AccountId id) : base(id)
        {
        }

        public void UpdateState(AccountBalance balance)
        {
            Emit(new AccountStateUpdatedEvent(balance));
        }

        public void OpenTradeDistributed(CopyTradeId copyTradeId)
        {
            if (!ActiveTrades.TryGetValue(copyTradeId, out var _))
            {
                Emit(new TradeOrderOpenDistributedEvent(TradeOrderId.New, copyTradeId), new Metadata(KeyValuePair.Create("copy-trade-id", copyTradeId.Value)));
            }
        }

        public void CloseTradeDistributed(CopyTradeId copyTradeId)
        {
            if (ActiveTrades.TryGetValue(copyTradeId, out var entity))
            {
                Emit(new TradeOrderCloseDistributedEvent(entity.Id, entity.CopyTradeId), new Metadata(KeyValuePair.Create("copy-trade-id", copyTradeId.Value)));
                Emit(new TradeOrderInactivatedEvent(entity.Id, entity.CopyTradeId), new Metadata(KeyValuePair.Create("copy-trade-id", copyTradeId.Value)));
            }
        }

        public void Apply(AccountStateUpdatedEvent e)
        {
        }

        public void Apply(TradeOrderOpenDistributedEvent e)
        {
            ActiveTrades.Add(e.CopyTradeId, new TradeOrderEntity(e.TradeOrderId, e.CopyTradeId, null, null, null));
        }

        public void Apply(TradeOrderCloseDistributedEvent e)
        {
        }

        public void Apply(TradeOrderInactivatedEvent e)
        {
            ActiveTrades.Remove(e.CopyTradeId);
        }
    }
}
