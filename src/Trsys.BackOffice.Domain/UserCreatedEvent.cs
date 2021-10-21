using EventFlow.Aggregates;

namespace Trsys.BackOffice.Domain
{
    public class UserCreatedEvent : AggregateEvent<UserAggregate, UserId>
    {
        public UserCreatedEvent(Username username, UserNickname nickname)
        {
            Username = username;
            Nickname = nickname;
        }

        public Username Username { get; }
        public UserNickname Nickname { get; }
    }
}