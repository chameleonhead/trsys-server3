using EventFlow.Aggregates;
using Trsys.Core;

namespace Trsys.BackOffice.Domain
{
    public class SubscriberDeletedEvent : AggregateEvent<SubscriberAggregate, SubscriberId>
    {
    }
}