using EventFlow.Aggregates;
using System.Collections.Generic;
using System.Linq;
using Trsys.Core;

namespace Trsys.Frontend.Domain
{
    public class SubscriberEaAggregate : AggregateRoot<SubscriberEaAggregate, SubscriberEaId>,
        IEmit<SubscriberEaRegisteredEvent>,
        IEmit<SubscriberEaUnregisteredEvent>,
        IEmit<SubscriberEaOrderTextChangedEvent>,
        IEmit<SubscriberEaDistributedOrderTextChangedEvent>
    {
        public EaOrderText Text { get; private set; } = EaOrderText.Empty;
        public EaOrderText DistributedText { get; private set; }
        public SecretKey Key { get; private set; }
        public int LastTikcetNumberSequence { get; private set; }

        public Dictionary<CopyTradeId, EaTicketNumber> TicketNumberByCopyTradeId { get; } = new();
        public Dictionary<EaTicketNumber, CopyTradeId> CopyTradeIdByTicketNumber { get; } = new();

        public SubscriberEaAggregate(SubscriberEaId id) : base(id)
        {
        }

        public void Register(SecretKey key, DistributionGroupId distributionGroupId, SubscriberId subscriberId)
        {
            Emit(new SubscriberEaRegisteredEvent(key, distributionGroupId, subscriberId));
        }

        public void Unregister(DistributionGroupId distributionGroupId, SubscriberId subscriberId)
        {
            Emit(new SubscriberEaUnregisteredEvent(Key, distributionGroupId, subscriberId));
        }

        public void Open(CopyTradeId copyTradeId, ForexTradeSymbol symbol, OrderType orderType)
        {
            if (TicketNumberByCopyTradeId.ContainsKey(copyTradeId))
            {
                return;
            }
            var ticketNumber = new EaTicketNumber(++LastTikcetNumberSequence);
            Emit(new SubscriberEaTradeOrderOpenRequestAppliedEvent(Key, copyTradeId, ticketNumber, symbol, orderType));
            Emit(new SubscriberEaOrderTextChangedEvent(Key, EaOrderText.FromOrders(Text.ToOrders().Union(new[] { new EaOrder(ticketNumber, symbol, orderType) }))));
        }

        public void Close(CopyTradeId copyTradeId)
        {
            if (!TicketNumberByCopyTradeId.TryGetValue(copyTradeId, out var ticketNumber))
            {
                return;
            }
            Emit(new SubscriberEaTradeOrderCloseRequestAppliedEvent(Key, copyTradeId, ticketNumber));
            Emit(new SubscriberEaOrderTextChangedEvent(Key, EaOrderText.FromOrders(Text.ToOrders().Where(e => e.TicketNo != ticketNumber))));
        }

        public void DistributeOrderText(EaOrderText text)
        {
            if (DistributedText != text)
            {
                Emit(new SubscriberEaDistributedOrderTextChangedEvent(Key, text));
            }
        }

        public void Apply(SubscriberEaRegisteredEvent aggregateEvent)
        {
            Key = aggregateEvent.Key;
        }

        public void Apply(SubscriberEaUnregisteredEvent aggregateEvent)
        {
        }

        public void Apply(SubscriberEaTradeOrderOpenRequestAppliedEvent aggregateEvent)
        {
            LastTikcetNumberSequence = aggregateEvent.TicketNumber.Value;
            TicketNumberByCopyTradeId.Add(aggregateEvent.CopyTradeId, aggregateEvent.TicketNumber);
            CopyTradeIdByTicketNumber.Add(aggregateEvent.TicketNumber, aggregateEvent.CopyTradeId);
        }

        public void Apply(SubscriberEaTradeOrderCloseRequestAppliedEvent aggregateEvent)
        {
            TicketNumberByCopyTradeId.Remove(aggregateEvent.CopyTradeId);
            CopyTradeIdByTicketNumber.Remove(aggregateEvent.TicketNumber);
        }

        public void Apply(SubscriberEaOrderTextChangedEvent aggregateEvent)
        {
            Text = aggregateEvent.Text;
        }

        public void Apply(SubscriberEaDistributedOrderTextChangedEvent aggregateEvent)
        {
            DistributedText = aggregateEvent.Text;
        }
    }
}
