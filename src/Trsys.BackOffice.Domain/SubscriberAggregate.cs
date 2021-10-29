using EventFlow.Aggregates;
using System;

namespace Trsys.BackOffice.Domain
{
    public class SubscriberAggregate : AggregateRoot<SubscriberAggregate, SubscriberId>,
        IEmit<SubscriberNameChangedEvent>,
        IEmit<SubscriberDeletedEvent>
    {
        public SubscriberName SubscriberName { get; private set; }
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

        public void Delete()
        {
            Emit(new SubscriberDeletedEvent());
        }

        public void Apply(SubscriberNameChangedEvent aggregateEvent)
        {
            SubscriberName = aggregateEvent.Name;
        }

        public void Apply(SubscriberDeletedEvent aggregateEvent)
        {
            IsDeleted = true;
        }
    }
}