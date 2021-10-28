using EventFlow.Aggregates;

namespace Trsys.BackOffice.Domain
{
    public class DistributionGroupDeletedEvent : AggregateEvent<DistributionGroupAggregate, DistributionGroupId>
    {
    }
}