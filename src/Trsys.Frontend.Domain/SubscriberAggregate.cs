using EventFlow.Aggregates;
using System.Collections.Generic;
using System.Linq;
using Trsys.Core;

namespace Trsys.Frontend.Domain
{
    public class SubscriberAggregate : AggregateRoot<SubscriberAggregate, SubscriberId>,
        IEmit<SubscriberRegisteredEvent>,
        IEmit<SubscriberUnregisteredEvent>,
        IEmit<SubscriberOrderTextChangedEvent>,
        IEmit<SubscriberDistributedOrderTextChangedEvent>
    {
        public EaOrderText Text { get; private set; } = EaOrderText.Empty;
        public EaOrderText DistributedText { get; private set; }
        public SecretKey Key { get; private set; }
        public int LastTikcetNumberSequence { get; private set; }

        public Dictionary<CopyTradeId, EaTicketNumber> TicketNumberByCopyTradeId { get; } = new();
        public Dictionary<EaTicketNumber, CopyTradeId> CopyTradeIdByTicketNumber { get; } = new();

        public SubscriberAggregate(SubscriberId id) : base(id)
        {
        }

        public void Register(SecretKey key, DistributionGroupId distributionGroupId)
        {
            Emit(new SubscriberRegisteredEvent(key, distributionGroupId));
        }

        public void Unregister(DistributionGroupId distributionGroupId)
        {
            Emit(new SubscriberUnregisteredEvent(Key, distributionGroupId));
        }

        public void Open(CopyTradeId copyTradeId, ForexTradeSymbol symbol, OrderType orderType)
        {
            if (TicketNumberByCopyTradeId.ContainsKey(copyTradeId))
            {
                return;
            }
            var ticketNumber = new EaTicketNumber(++LastTikcetNumberSequence);
            Emit(new SubscriberTradeOrderOpenRequestAppliedEvent(Key, copyTradeId, ticketNumber, symbol, orderType));
            Emit(new SubscriberOrderTextChangedEvent(Key, EaOrderText.FromOrders(Text.ToOrders().Union(new[] { new EaOrder(ticketNumber, symbol, orderType) }))));
        }

        public void Close(CopyTradeId copyTradeId)
        {
            if (!TicketNumberByCopyTradeId.TryGetValue(copyTradeId, out var ticketNumber))
            {
                return;
            }
            Emit(new SubscriberTradeOrderCloseRequestAppliedEvent(Key, copyTradeId, ticketNumber));
            Emit(new SubscriberOrderTextChangedEvent(Key, EaOrderText.FromOrders(Text.ToOrders().Where(e => e.TicketNo != ticketNumber))));
        }

        public void DistributeOrderText(EaOrderText text)
        {
            if (DistributedText != text)
            {
                Emit(new SubscriberDistributedOrderTextChangedEvent(Key, text));
            }
        }

        public void Apply(SubscriberRegisteredEvent aggregateEvent)
        {
            Key = aggregateEvent.Key;
        }

        public void Apply(SubscriberUnregisteredEvent aggregateEvent)
        {
        }

        public void Apply(SubscriberTradeOrderOpenRequestAppliedEvent aggregateEvent)
        {
            LastTikcetNumberSequence = aggregateEvent.TicketNumber.Value;
            TicketNumberByCopyTradeId.Add(aggregateEvent.CopyTradeId, aggregateEvent.TicketNumber);
            CopyTradeIdByTicketNumber.Add(aggregateEvent.TicketNumber, aggregateEvent.CopyTradeId);
        }

        public void Apply(SubscriberTradeOrderCloseRequestAppliedEvent aggregateEvent)
        {
            TicketNumberByCopyTradeId.Remove(aggregateEvent.CopyTradeId);
            CopyTradeIdByTicketNumber.Remove(aggregateEvent.TicketNumber);
        }

        public void Apply(SubscriberOrderTextChangedEvent aggregateEvent)
        {
            Text = aggregateEvent.Text;
        }

        public void Apply(SubscriberDistributedOrderTextChangedEvent aggregateEvent)
        {
            DistributedText = aggregateEvent.Text;
        }
    }
}
