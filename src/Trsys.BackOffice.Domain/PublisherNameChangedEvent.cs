using EventFlow.Aggregates;
using Trsys.Core;

namespace Trsys.BackOffice.Domain
{
    public class PublisherNameChangedEvent : AggregateEvent<PublisherAggregate, PublisherId>
    {
        public PublisherNameChangedEvent(PublisherName name)
        {
            Name = name;
        }

        public PublisherName Name { get; }
    }
}