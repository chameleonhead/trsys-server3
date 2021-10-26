using EventFlow.Aggregates;

namespace Trsys.BackOffice.Domain
{
    public class UserRoleRemovedEvent : AggregateEvent<UserAggregate, UserId>
    {
        public Role Role { get; internal set; }
    }
}