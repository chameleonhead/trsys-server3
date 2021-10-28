using EventFlow.Aggregates;

namespace Trsys.BackOffice.Domain
{
    public class UserDeletedEvent : AggregateEvent<UserAggregate, UserId>
    {
        public UserDeletedEvent(Username username)
        {
            Username = username;
        }

        public Username Username { get; }
    }
}