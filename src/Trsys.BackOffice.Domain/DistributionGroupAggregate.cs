using EventFlow.Aggregates;
using System;

namespace Trsys.BackOffice.Domain
{
    public class DistributionGroupAggregate : AggregateRoot<DistributionGroupAggregate, DistributionGroupId>,
        IEmit<DistributionGroupDisplayNameChangedEvent>,
        IEmit<DistributionGroupPublisherAddedEvent>,
        IEmit<DistributionGroupPublisherRemovedEvent>,
        IEmit<DistributionGroupSubscriberAddedEvent>,
        IEmit<DistributionGroupSubscriberRemovedEvent>,
        IEmit<DistributionGroupDeletedEvent>
    {
        public DistributionGroupDisplayName DisplayName { get; private set; }
        public bool IsDeleted { get; private set; }

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

        public void SetDisplayName(DistributionGroupDisplayName displayName)
        {
            EnsureNotDeleted();
            if (DisplayName != displayName)
            {
                Emit(new DistributionGroupDisplayNameChangedEvent(displayName));
            }
        }

        public void Delete()
        {
            Emit(new DistributionGroupDeletedEvent());
        }

        public void Apply(DistributionGroupDisplayNameChangedEvent aggregateEvent)
        {
            DisplayName = aggregateEvent.DisplayName;
        }

        public void Apply(DistributionGroupPublisherAddedEvent aggregateEvent)
        {
            throw new NotImplementedException();
        }

        public void Apply(DistributionGroupPublisherRemovedEvent aggregateEvent)
        {
            throw new NotImplementedException();
        }

        public void Apply(DistributionGroupSubscriberAddedEvent aggregateEvent)
        {
            throw new NotImplementedException();
        }

        public void Apply(DistributionGroupSubscriberRemovedEvent aggregateEvent)
        {
            throw new NotImplementedException();
        }

        public void Apply(DistributionGroupDeletedEvent aggregateEvent)
        {
            IsDeleted = true;
        }
    }
}