using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using Trsys.Core;

namespace Trsys.BackOffice.Domain
{
    public class DistributionGroupAggregate : AggregateRoot<DistributionGroupAggregate, DistributionGroupId>,
        IEmit<DistributionGroupNameChangedEvent>,
        IEmit<DistributionGroupPublisherAddedEvent>,
        IEmit<DistributionGroupPublisherRemovedEvent>,
        IEmit<DistributionGroupSubscriberAddedEvent>,
        IEmit<DistributionGroupSubscriberRemovedEvent>,
        IEmit<DistributionGroupDeletedEvent>
    {
        public DistributionGroupName DistributionGroupName { get; private set; }
        public bool IsDeleted { get; private set; }
        public HashSet<SubscriberId> Subscribers { get; } = new();
        public HashSet<PublisherId> Publishers { get; } = new();

        public DistributionGroupAggregate(DistributionGroupId id) : base(id)
        {
        }

        private void EnsureNotDeleted()
        {
            if (IsDeleted)
            {
                throw new InvalidOperationException();
            }
        }

        public void SetName(DistributionGroupName name)
        {
            EnsureNotDeleted();
            if (DistributionGroupName != name)
            {
                Emit(new DistributionGroupNameChangedEvent(name));
            }
        }

        public void Delete()
        {
            Emit(new DistributionGroupDeletedEvent());
        }

        public void AddPublisher(PublisherId publisherId)
        {
            if (Publishers.Contains(publisherId))
            {
                return;
            }
            Emit(new DistributionGroupPublisherAddedEvent(publisherId));
        }

        public void RemovePublisher(PublisherId publisherId)
        {
            if (Publishers.Contains(publisherId))
            {
                return;
            }
            Emit(new DistributionGroupPublisherRemovedEvent(publisherId));
        }

        public void AddSubscriber(SubscriberId subscriberId)
        {
            if (Subscribers.Contains(subscriberId))
            {
                return;
            }
            Emit(new DistributionGroupSubscriberAddedEvent(subscriberId));
        }

        public void RemoveSubscriber(SubscriberId subscriberId)
        {
            if (!Subscribers.Contains(subscriberId))
            {
                return;
            }
            Emit(new DistributionGroupSubscriberRemovedEvent(subscriberId));
        }

        public void Apply(DistributionGroupNameChangedEvent aggregateEvent)
        {
            DistributionGroupName = aggregateEvent.Name;
        }

        public void Apply(DistributionGroupPublisherAddedEvent aggregateEvent)
        {
            Publishers.Add(aggregateEvent.PublisherId);
        }

        public void Apply(DistributionGroupPublisherRemovedEvent aggregateEvent)
        {
            Publishers.Add(aggregateEvent.PublisherId);
        }

        public void Apply(DistributionGroupSubscriberAddedEvent aggregateEvent)
        {
            Subscribers.Add(aggregateEvent.SubscriberId);
        }

        public void Apply(DistributionGroupSubscriberRemovedEvent aggregateEvent)
        {
            Subscribers.Remove(aggregateEvent.SubscriberId);
        }

        public void Apply(DistributionGroupDeletedEvent aggregateEvent)
        {
            IsDeleted = true;
        }
    }
}