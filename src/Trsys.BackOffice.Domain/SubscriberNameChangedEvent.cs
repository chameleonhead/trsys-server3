using EventFlow.Aggregates;

namespace Trsys.BackOffice.Domain
{
    public class SubscriberNameChangedEvent : AggregateEvent<SubscriberAggregate, SubscriberId>
    {
        public SubscriberNameChangedEvent(SubscriberName name)
        {
            Name = name;
        }

        public SubscriberName Name { get; }
    }
}