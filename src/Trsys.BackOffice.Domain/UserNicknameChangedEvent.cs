using EventFlow.Aggregates;

namespace Trsys.BackOffice.Domain
{
    public class UserNicknameChangedEvent : AggregateEvent<UserAggregate, UserId>
    {
        public UserNicknameChangedEvent(UserNickname nickname)
        {
            Nickname = nickname;
        }

        public UserNickname Nickname { get; }
    }
}