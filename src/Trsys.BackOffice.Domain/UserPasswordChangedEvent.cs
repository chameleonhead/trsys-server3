using EventFlow.Aggregates;

namespace Trsys.BackOffice.Domain
{
    public class UserPasswordChangedEvent : AggregateEvent<UserAggregate, UserId>
    {
        public UserPasswordChangedEvent(HashedPassword password)
        {
            Password = password;
        }

        public HashedPassword Password { get; }
    }
}