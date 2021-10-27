using EventFlow.Aggregates;

namespace Trsys.BackOffice.Domain
{
    public class UserRoleRemovedEvent : AggregateEvent<UserAggregate, UserId>
    {
        public UserRoleRemovedEvent(Role role)
        {
            Role = role;
        }

        public Role Role { get; }
    }
}