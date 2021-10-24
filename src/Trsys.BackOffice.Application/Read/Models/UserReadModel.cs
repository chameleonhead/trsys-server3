using EventFlow.Aggregates;
using EventFlow.ReadStores;
using Trsys.BackOffice.Domain;

namespace Trsys.BackOffice.Application.Read.Models
{
    public class UserReadModel : IReadModel,
        IAmReadModelFor<UserAggregate, UserId, UserCreatedEvent>,
        IAmReadModelFor<UserAggregate, UserId, UserRoleAddedEvent>
    {
        public string Username { get; private set; }
        public string Nickname { get; private set; }
        public string Role { get; private set; }

        public void Apply(IReadModelContext context, IDomainEvent<UserAggregate, UserId, UserCreatedEvent> domainEvent)
        {
            Username = domainEvent.AggregateEvent.Username.Value;
            Nickname = domainEvent.AggregateEvent.Nickname.Value;
        }

        public void Apply(IReadModelContext context, IDomainEvent<UserAggregate, UserId, UserRoleAddedEvent> domainEvent)
        {
            Role = domainEvent.AggregateEvent.Role.Value;
        }
    }
}
