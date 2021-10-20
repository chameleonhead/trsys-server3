using EventFlow.Aggregates;
using EventFlow.ReadStores;
using System.Collections.Generic;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application.Read.Models
{
    public class SubscriberEaReadModel : IReadModel,
        IAmReadModelFor<SubscriberEaAggregate, SubscriberEaId, SubscriberEaRegisteredEvent>,
        IAmReadModelFor<SubscriberEaAggregate, SubscriberEaId, SubscriberEaUnregisteredEvent>,
        IAmReadModelFor<SubscriberEaAggregate, SubscriberEaId, SubscriberEaOrderTextChangedEvent>
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public Dictionary<string, string> AccountIdByDistributionGroupId { get; } = new();
        public string Text { get; set; }

        public string GetAccountId(string distributionGroupId)
        {
            AccountIdByDistributionGroupId.TryGetValue(distributionGroupId, out var accountId);
            return accountId;
        }

        public void Apply(IReadModelContext context, IDomainEvent<SubscriberEaAggregate, SubscriberEaId, SubscriberEaRegisteredEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;
            Key = domainEvent.AggregateEvent.Key.Value;
            AccountIdByDistributionGroupId[domainEvent.AggregateEvent.DistributionGroupId.Value] = domainEvent.AggregateEvent.AccountId.Value;
        }

        public void Apply(IReadModelContext context, IDomainEvent<SubscriberEaAggregate, SubscriberEaId, SubscriberEaUnregisteredEvent> domainEvent)
        {
            AccountIdByDistributionGroupId.Remove(domainEvent.AggregateEvent.DistributionGroupId.Value);
        }

        public void Apply(IReadModelContext context, IDomainEvent<SubscriberEaAggregate, SubscriberEaId, SubscriberEaOrderTextChangedEvent> domainEvent)
        {
            Text = domainEvent.AggregateEvent.Text.Value;
        }
    }
}
