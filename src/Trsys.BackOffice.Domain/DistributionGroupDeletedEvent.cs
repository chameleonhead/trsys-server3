using EventFlow.Aggregates;
using Trsys.Core;

namespace Trsys.BackOffice.Domain
{
    public class DistributionGroupDeletedEvent : AggregateEvent<DistributionGroupAggregate, DistributionGroupId>
    {
    }
}