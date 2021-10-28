using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Trsys.BackOffice.Domain
{
    public class UserAggregate : AggregateRoot<UserAggregate, UserId>,
        IEmit<UserUsernameChangedEvent>,
        IEmit<UserPasswordChangedEvent>,
        IEmit<UserNicknameChangedEvent>,
        IEmit<UserInChargeDistributionGroupAddedEvent>,
        IEmit<UserInChargeDistributionGroupRemovedEvent>,
        IEmit<UserRoleAddedEvent>,
        IEmit<UserRoleRemovedEvent>,
        IEmit<UserDeletedEvent>
    {
        public Username Username { get; private set; }
        public HashedPassword PasswordHash { get; private set; }
        public UserNickname Nickname { get; private set; }
        public List<DistributionGroupId> InChargeDistributionGroups { get; } = new();

        public List<Role> Roles { get; } = new();
        public bool IsDeleted { get; private set; }

        public UserAggregate(UserId id) : base(id)
        {
        }

        public void Delete()
        {
            Emit(new UserDeletedEvent(Username));
        }

        private void EnsureNotDeleted()
        {
            if (IsDeleted)
            {
                throw new InvalidOperationException();
            }
        }

        public void SetUsername(Username username)
        {
            EnsureNotDeleted();
            if (Username != username)
            {
                Emit(new UserUsernameChangedEvent(username));
            }
        }

        public void SetPasswordHash(HashedPassword passwordHash)
        {
            EnsureNotDeleted();
            if (PasswordHash != passwordHash)
            {
                Emit(new UserPasswordChangedEvent(passwordHash));
            }
        }

        public void SetNickname(UserNickname nickname)
        {
            EnsureNotDeleted();
            if (Nickname != nickname)
            {
                Emit(new UserNicknameChangedEvent(nickname));
            }
        }

        public void SetRoles(IEnumerable<Role> roles)
        {
            EnsureNotDeleted();
            foreach (var role in Roles.ToArray())
            {
                if (!roles.Contains(role))
                {
                    Emit(new UserRoleRemovedEvent(role));
                }
            }
            foreach (var role in roles)
            {
                if (!Roles.Contains(role))
                {
                    Emit(new UserRoleAddedEvent(role));
                }
            }
        }

        public void AddRole(Role role)
        {
            EnsureNotDeleted();
            if (!Roles.Contains(role))
            {
                Emit(new UserRoleAddedEvent(role));
            }
        }

        public void RemoveRole(Role role)
        {
            EnsureNotDeleted();
            if (Roles.Contains(role))
            {
                Emit(new UserRoleAddedEvent(role));
            }
        }

        public void SetInChargeDistributionGroup(List<DistributionGroupId> inChargeDistributionGroups)
        {
            EnsureNotDeleted();
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

        public void Apply(UserUsernameChangedEvent aggregateEvent)
        {
            Username = aggregateEvent.Username;
        }

        public void Apply(UserRoleAddedEvent aggregateEvent)
        {
            Roles.Add(aggregateEvent.Role);
        }

        public void Apply(UserRoleRemovedEvent aggregateEvent)
        {
            Roles.Remove(aggregateEvent.Role);
        }

        public void Apply(UserPasswordChangedEvent aggregateEvent)
        {
            PasswordHash = aggregateEvent.PasswordHash;
        }

        public void Apply(UserNicknameChangedEvent aggregateEvent)
        {
            Nickname = aggregateEvent.Nickname;
        }

        public void Apply(UserInChargeDistributionGroupAddedEvent aggregateEvent)
        {
            InChargeDistributionGroups.Add(aggregateEvent.DistributionGroupId);
        }

        public void Apply(UserInChargeDistributionGroupRemovedEvent aggregateEvent)
        {
            InChargeDistributionGroups.Remove(aggregateEvent.DistributionGroupId);
        }

        public void Apply(UserDeletedEvent aggregateEvent)
        {
            IsDeleted = true;
        }
    }
}