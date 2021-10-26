using EventFlow.Aggregates;

namespace Trsys.BackOffice.Domain
{
    public class UserPasswordChangedEvent : AggregateEvent<UserAggregate, UserId>
    {
        public UserPasswordChangedEvent(HashedPassword passwordHash)
        {
            PasswordHash = passwordHash;
        }

        public HashedPassword PasswordHash { get; }
    }
}