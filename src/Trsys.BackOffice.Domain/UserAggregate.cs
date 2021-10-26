using System.Collections.Generic;
using EventFlow.Aggregates;

namespace Trsys.BackOffice.Domain
{
    public class UserAggregate : AggregateRoot<UserAggregate, UserId>,
        IEmit<UserCreatedEvent>,
        IEmit<UserPasswordChangedEvent>,
        IEmit<UserInChargeDistributionGroupAddedEvent>
    {
        public Username Username { get; private set; }
        public HashedPassword PasswordHash { get; private set; }
        public List<DistributionGroupId> InChargeDistributionGroups { get; } = new();
        public List<Role> Roles { get; } = new();

        public UserAggregate(UserId id) : base(id)
        {
        }

        public void Create(Username username, UserNickname nickname)
        {
            Emit(new UserCreatedEvent(username, nickname), new Metadata(KeyValuePair.Create("username", username.Value)));
        }

        public void AddRole(Role role)
        {
            Emit(new UserRoleAddedEvent(role), new Metadata(KeyValuePair.Create("username", Username.Value)));
        }

        public void SetPasswordHash(HashedPassword passwordHash)
        {
            if (this.PasswordHash != passwordHash)
            {
                Emit(new UserPasswordChangedEvent(passwordHash), new Metadata(KeyValuePair.Create("username", Username.Value)));
            }
        }

        public void SetInChargeDistributionGroup(List<DistributionGroupId> inChargeDistributionGroups)
        {
            foreach (var distributionGroupId in InChargeDistributionGroups)
            {
                if (!inChargeDistributionGroups.Contains(distributionGroupId))
                {
                    Emit(new UserInChargeDistributionGroupRemovedEvent(distributionGroupId), new Metadata(KeyValuePair.Create("username", Username.Value)));
                }
            }
            foreach (var distributionGroupId in inChargeDistributionGroups)
            {
                if (!InChargeDistributionGroups.Contains(distributionGroupId))
                {
                    Emit(new UserInChargeDistributionGroupAddedEvent(distributionGroupId), new Metadata(KeyValuePair.Create("username", Username.Value)));
                }
            }
        }

        public void Apply(UserCreatedEvent aggregateEvent)
        {
            Username = aggregateEvent.Username;
        }

        public void Apply(UserRoleAddedEvent aggregateEvent)
        {
            Roles.Add(aggregateEvent.Role);
        }

        public void Apply(UserPasswordChangedEvent aggregateEvent)
        {
            PasswordHash = aggregateEvent.PasswordHash;
        }

        public void Apply(UserInChargeDistributionGroupAddedEvent aggregateEvent)
        {
            InChargeDistributionGroups.Add(aggregateEvent.DistributionGroupId);
        }

        public void Apply(UserInChargeDistributionGroupRemovedEvent aggregateEvent)
        {
            InChargeDistributionGroups.Remove(aggregateEvent.DistributionGroupId);
        }
    }
}