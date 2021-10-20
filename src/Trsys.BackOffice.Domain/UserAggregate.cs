using EventFlow.Aggregates;

namespace Trsys.BackOffice.Domain
{
    public class UserAggregate : AggregateRoot<UserAggregate, UserId>,
        IEmit<UserCreatedEvent>
    {
        public UserAggregate(UserId id) : base(id)
        {
        }

        public void Create(Username username, HashedPassword password)
        {
            Emit(new UserCreatedEvent(username, password));
        }

        public void Apply(UserCreatedEvent aggregateEvent)
        {
        }
    }
}