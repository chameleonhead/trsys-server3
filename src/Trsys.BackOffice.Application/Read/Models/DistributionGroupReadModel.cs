using EventFlow.Aggregates;
using EventFlow.ReadStores;
using Trsys.BackOffice.Domain;

namespace Trsys.BackOffice.Application.Read.Models
{
    public class DistributionGroupReadModel : IReadModel,
        IAmReadModelFor<DistributionGroupAggregate, DistributionGroupId, DistributionGroupNameChangedEvent>,
        IAmReadModelFor<DistributionGroupAggregate, DistributionGroupId, DistributionGroupDeletedEvent>
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public void Apply(IReadModelContext context, IDomainEvent<DistributionGroupAggregate, DistributionGroupId, DistributionGroupNameChangedEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;
            Name = domainEvent.AggregateEvent.Name.Value;
        }

        public void Apply(IReadModelContext context, IDomainEvent<DistributionGroupAggregate, DistributionGroupId, DistributionGroupDeletedEvent> domainEvent)
        {
            context.MarkForDeletion();
        }
    }
}
