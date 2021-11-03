using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using Trsys.Core;

namespace Trsys.Frontend.Domain
{
    public class PublisherAggregate : AggregateRoot<PublisherAggregate, PublisherId>,
        IEmit<PublisherRegisteredEvent>,
        IEmit<PublisherUnregisteredEvent>,
        IEmit<PublisherOrderTextChangedEvent>,
        IEmit<PublisherOpenedOrderEvent>,
        IEmit<PublisherClosedOrderEvent>
    {
        public SecretKey Key { get; private set; }
        public HashSet<DistributionGroupId> DistributionGroups { get; } = new();
        public EaOrderText Text { get; private set; }
        public Dictionary<EaOrderId, PublisherOrderEntity> OrdersById { get; } = new();
        public Dictionary<EaTicketNumber, PublisherOrderEntity> OrdersByTicketNumber { get; } = new();

        public PublisherAggregate(PublisherId id) : base(id)
        {
        }

        public void Register(SecretKey key, DistributionGroupId distributionGroupId)
        {
            if (!DistributionGroups.Contains(distributionGroupId))
            {
                Emit(new PublisherRegisteredEvent(key, distributionGroupId));
            }
        }

        public void Unregister(DistributionGroupId distributionGroupId)
        {
            if (DistributionGroups.Contains(distributionGroupId))
            {
                Emit(new PublisherUnregisteredEvent(Key, distributionGroupId));
            }
        }

        public void UpdateOrderText(EaOrderText text)
        {
            if (Text != text)
            {
                Emit(new PublisherOrderTextChangedEvent(Key, text));
                var prevOrderTicketNos = OrdersByTicketNumber.Keys;
                var nextOrders = text.ToOrders().ToHashSet();
                var nextOrderTicketNos = nextOrders.Select(e => e.TicketNo).ToHashSet();

                foreach (var prevTicketNo in prevOrderTicketNos)
                {
                    if (!nextOrderTicketNos.Contains(prevTicketNo))
                    {
                        var order = OrdersByTicketNumber[prevTicketNo];
                        Emit(new PublisherClosedOrderEvent(order));
                    }
                }
                foreach (var order in nextOrders)
                {
                    if (!prevOrderTicketNos.Contains(order.TicketNo))
                    {
                        Emit(new PublisherOpenedOrderEvent(new PublisherOrderEntity(EaOrderId.New, order.TicketNo, order.Symbol, order.OrderType, DistributionGroups.Select(distributionGroupId => new PublisherCopyTradeEntity(CopyTradeId.New, distributionGroupId)).ToList())));
                    }
                }
            }
        }

        public void Apply(PublisherRegisteredEvent aggregateEvent)
        {
            Key = aggregateEvent.Key;
            DistributionGroups.Add(aggregateEvent.DistributionGroupId);
        }

        public void Apply(PublisherUnregisteredEvent aggregateEvent)
        {
            DistributionGroups.Remove(aggregateEvent.DistributionGroupId);
        }

        public void Apply(PublisherOrderTextChangedEvent aggregateEvent)
        {
            Text = aggregateEvent.Text;
        }

        public void Apply(PublisherOpenedOrderEvent aggregateEvent)
        {
            var entity = aggregateEvent.Order;
            OrdersByTicketNumber.Add(entity.TicketNo, entity);
            OrdersById.Add(entity.Id, entity);
        }

        public void Apply(PublisherClosedOrderEvent aggregateEvent)
        {
            var entity = aggregateEvent.Order;
            OrdersByTicketNumber.Remove(entity.TicketNo);
            OrdersById.Remove(entity.Id);
        }
    }
}
