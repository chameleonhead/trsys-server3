using System.Collections.Generic;
using EventFlow.Aggregates;
using Trsys.CopyTrading.Domain;

namespace Trsys.BackOffice.Domain
{
    public class UserAggregate : AggregateRoot<UserAggregate, UserId>,
        IEmit<UserCreatedEvent>,
        IEmit<UserPasswordChangedEvent>,
        IEmit<UserInChargeDistributionGroupAddedEvent>
    {
        public HashedPassword Password { get; private set; }
        public List<DistributionGroupId> InChargeDistributionGroups { get; } = new();
        public List<Role> Roles { get; } = new();

        public UserAggregate(UserId id) : base(id)
        {
        }

        public void Create(Username username, UserNickname nickname)
        {
            Emit(new UserCreatedEvent(username, nickname));
        }

        public void AddRole(Role role)
        {
            Emit(new UserRoleAddedEvent(role));
        }

        public void SetPassword(HashedPassword password)
        {
            if (this.Password != password)
            {
                Emit(new UserPasswordChangedEvent(password));
            }
        }

        public void SetInChargeDistributionGroup(List<DistributionGroupId> inChargeDistributionGroups)
        {
            foreach (var distributionGroupId in InChargeDistributionGroups)
            {
                if (!inChargeDistributionGroups.Contains(distributionGroupId))
                {
                    Emit(new UserInChargeDistributionGroupRemovedEvent(distributionGroupId));
                }
            }
            foreach (var distributionGroupId in inChargeDistributionGroups)
            {
                if (!InChargeDistributionGroups.Contains(distributionGroupId))
                {
                    Emit(new UserInChargeDistributionGroupAddedEvent(distributionGroupId));
                }
            }
        }

        public void Apply(UserCreatedEvent aggregateEvent)
        {
        }

        public void Apply(UserRoleAddedEvent aggregateEvent)
        {
            Roles.Add(aggregateEvent.Role);
        }

        public void Apply(UserPasswordChangedEvent aggregateEvent)
        {
            Password = aggregateEvent.Password;
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