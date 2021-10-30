using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class DistributionGroupSubscriberAddedEvent: AggregateEvent<DistributionGroupAggregate, DistributionGroupId>
    {
        public DistributionGroupSubscriberAddedEvent(SubscriberId accountId)
        {
            AccountId = accountId;
        }

        public SubscriberId AccountId { get; }
    }
}