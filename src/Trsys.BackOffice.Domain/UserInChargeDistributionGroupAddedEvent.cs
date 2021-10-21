using EventFlow.Aggregates;
using Trsys.CopyTrading.Domain;

namespace Trsys.BackOffice.Domain
{
    public class UserInChargeDistributionGroupAddedEvent : AggregateEvent<UserAggregate, UserId>
    {
        public UserInChargeDistributionGroupAddedEvent(DistributionGroupId distributionGroupId)
        {
            DistributionGroupId = distributionGroupId;
        }

        public DistributionGroupId DistributionGroupId { get; }
    }
}