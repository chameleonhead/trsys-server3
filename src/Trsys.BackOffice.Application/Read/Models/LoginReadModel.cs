using EventFlow.Aggregates;
using EventFlow.ReadStores;
using Trsys.BackOffice.Domain;

namespace Trsys.BackOffice.Application.Read.Models
{
    public class LoginReadModel : IReadModel,
        IAmReadModelFor<UserAggregate, UserId, UserCreatedEvent>,
        IAmReadModelFor<UserAggregate, UserId, UserPasswordChangedEvent>
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }

        public void Apply(IReadModelContext context, IDomainEvent<UserAggregate, UserId, UserCreatedEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;
            Username = domainEvent.AggregateEvent.Username.Value;
        }

        public void Apply(IReadModelContext context, IDomainEvent<UserAggregate, UserId, UserPasswordChangedEvent> domainEvent)
        {
            PasswordHash = domainEvent.AggregateEvent.Password.Value;
        }
    }
}
