using EventFlow.Aggregates;

namespace Trsys.BackOffice.Domain
{
    public class UserCreatedEvent : AggregateEvent<UserAggregate, UserId>
    {
        public UserCreatedEvent(Username username, HashedPassword password)
        {
            Username = username;
            Password = password;
        }

        public Username Username { get; }
        public HashedPassword Password { get; }
    }
}