using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using Trsys.CopyTrading.Abstractions;

namespace Trsys.Ea.Domain
{
    public class PublisherEaAggregate : AggregateRoot<PublisherEaAggregate, PublisherEaId>,
        IEmit<PublisherEaRegisteredEvent>,
        IEmit<PublisherEaUnregisteredEvent>,
        IEmit<PublisherEaOrderTextChangedEvent>,
        IEmit<PublisherEaOpenedOrderEvent>,
        IEmit<PublisherEaClosedOrderEvent>
    {
        public SecretKey Key { get; private set; }
        public List<PublisherEaDistributionTarget> Targets { get; } = new();
        public EaOrderText Text { get; private set; }
        public Dictionary<EaOrderId, PublisherEaOrderEntity> OrdersById { get; } = new();
        public Dictionary<EaTicketNumber, PublisherEaOrderEntity> OrdersByTicketNumber { get; } = new();

        public PublisherEaAggregate(PublisherEaId id) : base(id)
        {
        }

        public void Register(SecretKey key, DistributionGroupId distributionGroupId)
        {
            Emit(new PublisherEaRegisteredEvent(key, distributionGroupId));
        }

        public void Unregister(DistributionGroupId distributionGroupId)
        {
            Emit(new PublisherEaUnregisteredEvent(Key, distributionGroupId));
        }

        public void UpdateOrderText(EaOrderText text)
        {
            if (Text != text)
            {
                Emit(new PublisherEaOrderTextChangedEvent(Key, text));
                var prevOrderTicketNos = OrdersByTicketNumber.Keys;
                var nextOrders = text.ToOrders().ToHashSet();
                var nextOrderTicketNos = nextOrders.Select(e => e.TicketNo).ToHashSet();

                foreach (var prevTicketNo in prevOrderTicketNos)
                {
                    if (!nextOrderTicketNos.Contains(prevTicketNo))
                    {
                        var order = OrdersByTicketNumber[prevTicketNo];
                        Emit(new PublisherEaClosedOrderEvent(order));
                    }
                }
                foreach (var order in nextOrders)
                {
                    if (!prevOrderTicketNos.Contains(order.TicketNo))
                    {
                        Emit(new PublisherEaOpenedOrderEvent(new PublisherEaOrderEntity(EaOrderId.New, order.TicketNo, order.Symbol, order.OrderType, Targets.Select(t => new PublisherEaCopyTradeEntity(CopyTradeId.New, t.DistributionGroupId)).ToList())));
                    }
                }
            }
        }

        public void Apply(PublisherEaRegisteredEvent aggregateEvent)
        {
            Key = aggregateEvent.Key;
            Targets.Add(new PublisherEaDistributionTarget(aggregateEvent.DistributionGroupId));
        }

        public void Apply(PublisherEaUnregisteredEvent aggregateEvent)
        {
            Targets.Remove(new PublisherEaDistributionTarget(aggregateEvent.DistributionGroupId));
        }

        public void Apply(PublisherEaOrderTextChangedEvent aggregateEvent)
        {
            Text = aggregateEvent.Text;
        }

        public void Apply(PublisherEaOpenedOrderEvent aggregateEvent)
        {
            var entity = aggregateEvent.Order;
            OrdersByTicketNumber.Add(entity.TicketNo, entity);
            OrdersById.Add(entity.Id, entity);
        }

        public void Apply(PublisherEaClosedOrderEvent aggregateEvent)
        {
            var entity = aggregateEvent.Order;
            OrdersByTicketNumber.Remove(entity.TicketNo);
            OrdersById.Remove(entity.Id);
        }
    }
}
