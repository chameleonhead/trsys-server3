using EventFlow.Aggregates;
using EventFlow.ReadStores;
using System.Collections.Generic;
using Trsys.BackOffice.Domain;

namespace Trsys.BackOffice.Application.Read.Models
{
    public class UserReadModel : IReadModel,
        IAmReadModelFor<UserAggregate, UserId, UserCreatedEvent>,
        IAmReadModelFor<UserAggregate, UserId, UserRoleAddedEvent>,
        IAmReadModelFor<UserAggregate, UserId, UserInChargeDistributionGroupAddedEvent>
    {
        public string Username { get; private set; }
        public string Nickname { get; private set; }
        public List<string> Roles { get; } = new();
        public List<string> DistributionGroups { get; } = new();

        public void Apply(IReadModelContext context, IDomainEvent<UserAggregate, UserId, UserCreatedEvent> domainEvent)
        {
            Username = domainEvent.AggregateEvent.Username.Value;
            Nickname = domainEvent.AggregateEvent.Nickname.Value;
        }

        public void Apply(IReadModelContext context, IDomainEvent<UserAggregate, UserId, UserRoleAddedEvent> domainEvent)
        {
            Roles.Add(domainEvent.AggregateEvent.Role.Value);
        }

        public void Apply(IReadModelContext context, IDomainEvent<UserAggregate, UserId, UserInChargeDistributionGroupAddedEvent> domainEvent)
        {
            DistributionGroups.Add(domainEvent.AggregateEvent.DistributionGroupId.Value);
        }
    }
}
