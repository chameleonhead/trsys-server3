using EventFlow.Aggregates;
using EventFlow.ReadStores;
using Trsys.BackOffice.Domain;

namespace Trsys.BackOffice.Application.Read.Models
{
    public class DistributionGroupReadModel : IReadModel,
        IAmReadModelFor<DistributionGroupAggregate, DistributionGroupId, DistributionGroupDisplayNameChangedEvent>,
        IAmReadModelFor<DistributionGroupAggregate, DistributionGroupId, DistributionGroupDeletedEvent>
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }

        public void Apply(IReadModelContext context, IDomainEvent<DistributionGroupAggregate, DistributionGroupId, DistributionGroupDisplayNameChangedEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;
            DisplayName = domainEvent.AggregateEvent.DisplayName.Value;
        }

        public void Apply(IReadModelContext context, IDomainEvent<DistributionGroupAggregate, DistributionGroupId, DistributionGroupDeletedEvent> domainEvent)
        {
            context.MarkForDeletion();
        }
    }
}
