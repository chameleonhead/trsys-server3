using EventFlow.Aggregates;
using Trsys.Core;

namespace Trsys.BackOffice.Domain
{
    public class PublisherDeletedEvent : AggregateEvent<PublisherAggregate, PublisherId>
    {
    }
}