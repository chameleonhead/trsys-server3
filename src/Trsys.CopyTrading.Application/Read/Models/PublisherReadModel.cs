using EventFlow.Aggregates;
using EventFlow.ReadStores;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Read.Models
{
    public class PublisherReadModel : IReadModel,
        IAmReadModelFor<DistributionGroupAggregate, DistributionGroupId, PublisherAddedEvent>
    {
        public string Id { get; set; }
        public string DistributorGroupId { get; set; }
        public string ClientKey { get; set; }
        public void Apply(IReadModelContext context, IDomainEvent<DistributionGroupAggregate, DistributionGroupId, PublisherAddedEvent> domainEvent)
        {
            Id = domainEvent.AggregateEvent.PublisherId.Value;
            DistributorGroupId = domainEvent.AggregateIdentity.Value;
            ClientKey = domainEvent.AggregateEvent.ClientKey.Value;
        }
    }
}
