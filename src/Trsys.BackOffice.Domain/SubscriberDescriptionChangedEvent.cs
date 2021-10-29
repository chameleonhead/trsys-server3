using EventFlow.Aggregates;

namespace Trsys.BackOffice.Domain
{
    public class SubscriberDescriptionChangedEvent : AggregateEvent<SubscriberAggregate, SubscriberId>
    {
        public SubscriberDescriptionChangedEvent(SubscriberDescription description)
        {
            Description = description;
        }

        public SubscriberDescription Description { get; }
    }
}