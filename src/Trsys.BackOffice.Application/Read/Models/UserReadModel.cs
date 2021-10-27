using EventFlow.Aggregates;
using EventFlow.ReadStores;
using System.Collections.Generic;
using Trsys.BackOffice.Domain;

namespace Trsys.BackOffice.Application.Read.Models
{
    public class UserReadModel : IReadModel,
        IAmReadModelFor<UserAggregate, UserId, UserUsernameChangedEvent>,
        IAmReadModelFor<UserAggregate, UserId, UserPasswordChangedEvent>,
        IAmReadModelFor<UserAggregate, UserId, UserNicknameChangedEvent>,
        IAmReadModelFor<UserAggregate, UserId, UserRoleAddedEvent>,
        IAmReadModelFor<UserAggregate, UserId, UserRoleRemovedEvent>,
        IAmReadModelFor<UserAggregate, UserId, UserInChargeDistributionGroupAddedEvent>,
        IAmReadModelFor<UserAggregate, UserId, UserDeletedEvent>
    {
        public string Id { get; private set; }
        public string Username { get; private set; }
        public string PasswordHash { get; private set; }
        public string Nickname { get; private set; }
        public List<string> Roles { get; } = new();
        public List<string> DistributionGroups { get; } = new();

        public void Apply(IReadModelContext context, IDomainEvent<UserAggregate, UserId, UserUsernameChangedEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;
            Username = domainEvent.AggregateEvent.Username.Value;
        }

        public void Apply(IReadModelContext context, IDomainEvent<UserAggregate, UserId, UserNicknameChangedEvent> domainEvent)
        {
            Nickname = domainEvent.AggregateEvent.Nickname.Value;
        }

        public void Apply(IReadModelContext context, IDomainEvent<UserAggregate, UserId, UserPasswordChangedEvent> domainEvent)
        {
            PasswordHash = domainEvent.AggregateEvent.PasswordHash.Value;
        }

        public void Apply(IReadModelContext context, IDomainEvent<UserAggregate, UserId, UserRoleAddedEvent> domainEvent)
        {
            Roles.Add(domainEvent.AggregateEvent.Role.Value);
        }

        public void Apply(IReadModelContext context, IDomainEvent<UserAggregate, UserId, UserRoleRemovedEvent> domainEvent)
        {
            Roles.Remove(domainEvent.AggregateEvent.Role.Value);
        }

        public void Apply(IReadModelContext context, IDomainEvent<UserAggregate, UserId, UserInChargeDistributionGroupAddedEvent> domainEvent)
        {
            DistributionGroups.Add(domainEvent.AggregateEvent.DistributionGroupId.Value);
        }

        public void Apply(IReadModelContext context, IDomainEvent<UserAggregate, UserId, UserDeletedEvent> domainEvent)
        {
            context.MarkForDeletion();
        }
    }
}
