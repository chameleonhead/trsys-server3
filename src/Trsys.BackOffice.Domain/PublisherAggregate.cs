using EventFlow.Aggregates;
using System;

namespace Trsys.BackOffice.Domain
{
    public class PublisherAggregate : AggregateRoot<PublisherAggregate, PublisherId>,
        IEmit<PublisherNameChangedEvent>,
        IEmit<PublisherDeletedEvent>
    {
        public PublisherName PublisherName { get; private set; }
        public bool IsDeleted { get; private set; }

        public PublisherAggregate(PublisherId id) : base(id)
        {
        }

        private void EnsureNotDeleted()
        {
            if (IsDeleted)
            {
                throw new InvalidOperationException();
            }
        }

        public void SetName(PublisherName name)
        {
            EnsureNotDeleted();
            if (PublisherName != name)
            {
                Emit(new PublisherNameChangedEvent(name));
            }
        }

        public void Delete()
        {
            Emit(new PublisherDeletedEvent());
        }

        public void Apply(PublisherNameChangedEvent aggregateEvent)
        {
            PublisherName = aggregateEvent.Name;
        }

        public void Apply(PublisherDeletedEvent aggregateEvent)
        {
            IsDeleted = true;
        }
    }
}