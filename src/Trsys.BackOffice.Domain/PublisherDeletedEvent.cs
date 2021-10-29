using EventFlow.Aggregates;

namespace Trsys.BackOffice.Domain
{
    public class PublisherDeletedEvent : AggregateEvent<PublisherAggregate, PublisherId>
    {
    }
}