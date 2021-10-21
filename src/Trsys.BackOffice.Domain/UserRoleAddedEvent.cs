using EventFlow.Aggregates;

namespace Trsys.BackOffice.Domain
{
    public class UserRoleAddedEvent : AggregateEvent<UserAggregate, UserId>
    {
        public UserRoleAddedEvent(Role role)
        {
            Role = role;
        }

        public Role Role { get; }
    }
}