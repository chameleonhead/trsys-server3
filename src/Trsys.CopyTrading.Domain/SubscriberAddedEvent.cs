using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class SubscriberAddedEvent: AggregateEvent<DistributionGroupAggregate, DistributionGroupId>
    {
        public SubscriberAddedEvent(AccountId accountId)
        {
            AccountId = accountId;
        }

        public AccountId AccountId { get; }
    }
}