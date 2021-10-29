using EventFlow.Aggregates;

namespace Trsys.BackOffice.Domain
{
    public class SubscriberDeletedEvent : AggregateEvent<SubscriberAggregate, SubscriberId>
    {
    }
}