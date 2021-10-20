using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class DistributionGroupSubscriberAddedEvent: AggregateEvent<DistributionGroupAggregate, DistributionGroupId>
    {
        public DistributionGroupSubscriberAddedEvent(AccountId accountId)
        {
            AccountId = accountId;
        }

        public AccountId AccountId { get; }
    }
}