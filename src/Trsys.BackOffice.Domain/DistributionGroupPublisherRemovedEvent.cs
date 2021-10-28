using EventFlow.Aggregates;

namespace Trsys.BackOffice.Domain
{
    public class DistributionGroupPublisherRemovedEvent : AggregateEvent<DistributionGroupAggregate, DistributionGroupId>
    {
        public DistributionGroupPublisherRemovedEvent(PublisherId publisherId)
        {
            PublisherId = publisherId;
        }

        public PublisherId PublisherId { get; }
    }
}