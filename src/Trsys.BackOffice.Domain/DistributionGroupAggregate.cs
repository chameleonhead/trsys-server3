using EventFlow.Aggregates;
using System;

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

        public void Apply(DistributionGroupNameChangedEvent aggregateEvent)
        {
            DistributionGroupName = aggregateEvent.Name;
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