using EventFlow.Aggregates;

namespace Trsys.BackOffice.Domain
{
    public class UserUsernameChangedEvent : AggregateEvent<UserAggregate, UserId>
    {
        public UserUsernameChangedEvent(Username username)
        {
            Username = username;
        }

        public Username Username { get; }
    }
}