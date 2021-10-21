using EventFlow.Aggregates;
using Trsys.CopyTrading.Domain;

namespace Trsys.BackOffice.Domain
{
    public class UserInChargeDistributionGroupRemovedEvent : AggregateEvent<UserAggregate, UserId>
    {
        public UserInChargeDistributionGroupRemovedEvent(DistributionGroupId distributionGroupId)
        {
            DistributionGroupId = distributionGroupId;
        }

        public DistributionGroupId DistributionGroupId { get; }
    }
}