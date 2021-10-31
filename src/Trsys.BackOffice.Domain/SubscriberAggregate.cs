using EventFlow.Aggregates;
using System;
using Trsys.Core;

namespace Trsys.BackOffice.Domain
{
    public class SubscriberAggregate : AggregateRoot<SubscriberAggregate, SubscriberId>,
        IEmit<SubscriberNameChangedEvent>,
        IEmit<SubscriberDescriptionChangedEvent>,
        IEmit<SubscriberDeletedEvent>
    {
        public SubscriberName SubscriberName { get; private set; }
        public SubscriberDescription Description { get; private set; }
        public bool IsDeleted { get; private set; }

        public SubscriberAggregate(SubscriberId id) : base(id)
        {
        }

        private void EnsureNotDeleted()
        {
            if (IsDeleted)
            {
                throw new InvalidOperationException();
            }
        }

        public void SetName(SubscriberName name)
        {
            EnsureNotDeleted();
            if (SubscriberName != name)
            {
                Emit(new SubscriberNameChangedEvent(name));
            }
        }

        public void SetDescription(SubscriberDescription description)
        {
            EnsureNotDeleted();
            if (Description != description)
            {
                Emit(new SubscriberDescriptionChangedEvent(description));
            }
        }

        public void Delete()
        {
            Emit(new SubscriberDeletedEvent());
        }

        public void Apply(SubscriberNameChangedEvent aggregateEvent)
        {
            SubscriberName = aggregateEvent.Name;
        }

        public void Apply(SubscriberDescriptionChangedEvent aggregateEvent)
        {
            Description = aggregateEvent.Description;
        }

        public void Apply(SubscriberDeletedEvent aggregateEvent)
        {
            IsDeleted = true;
        }
    }
}