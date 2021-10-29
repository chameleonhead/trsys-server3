using EventFlow.Aggregates;

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