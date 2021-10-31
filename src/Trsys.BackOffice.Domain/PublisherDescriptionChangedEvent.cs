using EventFlow.Aggregates;
using Trsys.Core;

namespace Trsys.BackOffice.Domain
{
    public class PublisherDescriptionChangedEvent : AggregateEvent<PublisherAggregate, PublisherId>
    {
        public PublisherDescriptionChangedEvent(PublisherDescription description)
        {
            Description = description;
        }

        public PublisherDescription Description { get; }
    }
}