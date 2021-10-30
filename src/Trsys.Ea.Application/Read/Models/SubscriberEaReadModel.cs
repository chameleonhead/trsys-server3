using EventFlow.Aggregates;
using EventFlow.ReadStores;
using System.Collections.Generic;
using Trsys.Ea.Domain;

namespace Trsys.Ea.Application.Read.Models
{
    public class SubscriberEaReadModel : IReadModel,
        IAmReadModelFor<SubscriberEaAggregate, SubscriberEaId, SubscriberEaRegisteredEvent>,
        IAmReadModelFor<SubscriberEaAggregate, SubscriberEaId, SubscriberEaUnregisteredEvent>,
        IAmReadModelFor<SubscriberEaAggregate, SubscriberEaId, SubscriberEaOrderTextChangedEvent>
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public Dictionary<string, string> SubscriberIdByDistributionGroupId { get; } = new();
        public string Text { get; set; }

        public string GetSubscriberId(string distributionGroupId)
        {
            SubscriberIdByDistributionGroupId.TryGetValue(distributionGroupId, out var subscriberId);
            return subscriberId;
        }

        public void Apply(IReadModelContext context, IDomainEvent<SubscriberEaAggregate, SubscriberEaId, SubscriberEaRegisteredEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;
            Key = domainEvent.AggregateEvent.Key.Value;
            SubscriberIdByDistributionGroupId[domainEvent.AggregateEvent.DistributionGroupId.Value] = domainEvent.AggregateEvent.SubscriberId.Value;
        }

        public void Apply(IReadModelContext context, IDomainEvent<SubscriberEaAggregate, SubscriberEaId, SubscriberEaUnregisteredEvent> domainEvent)
        {
            SubscriberIdByDistributionGroupId.Remove(domainEvent.AggregateEvent.DistributionGroupId.Value);
        }

        public void Apply(IReadModelContext context, IDomainEvent<SubscriberEaAggregate, SubscriberEaId, SubscriberEaOrderTextChangedEvent> domainEvent)
        {
            Text = domainEvent.AggregateEvent.Text.Value;
        }
    }
}
