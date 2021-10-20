using EventFlow.Aggregates;
using EventFlow.ReadStores;
using System.Collections.Generic;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Read.Models
{
    public class PublisherEaReadModel : IReadModel,
        IAmReadModelFor<PublisherEaAggregate, PublisherEaId, PublisherEaRegisteredEvent>,
        IAmReadModelFor<PublisherEaAggregate, PublisherEaId, PublisherEaUnregisteredEvent>
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public Dictionary<string, string> PublisherIdByDistributionGroupId { get; } = new();

        public void Apply(IReadModelContext context, IDomainEvent<PublisherEaAggregate, PublisherEaId, PublisherEaRegisteredEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;
            Key = domainEvent.AggregateEvent.Key.Value;
            PublisherIdByDistributionGroupId[domainEvent.AggregateEvent.DistributionGroupId.Value] = domainEvent.AggregateEvent.PublisherId.Value;
        }

        public void Apply(IReadModelContext context, IDomainEvent<PublisherEaAggregate, PublisherEaId, PublisherEaUnregisteredEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;
            Key = domainEvent.AggregateEvent.Key.Value;
            PublisherIdByDistributionGroupId.Remove(domainEvent.AggregateEvent.DistributionGroupId.Value);
        }

        public string GetPublisherId(string distributionGroupId)
        {
            PublisherIdByDistributionGroupId.TryGetValue(distributionGroupId, out var publisherId);
            return publisherId;
        }
    }
}
