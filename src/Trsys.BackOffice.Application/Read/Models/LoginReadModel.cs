using EventFlow.Aggregates;
using EventFlow.ReadStores;
using Trsys.BackOffice.Domain;

namespace Trsys.BackOffice.Application.Read.Models
{
    public class LoginReadModel : IReadModel,
        IAmReadModelFor<UserAggregate, UserId, UserUsernameChangedEvent>,
        IAmReadModelFor<UserAggregate, UserId, UserDeletedEvent>
    {
        public string Id { get; set; }
        public string Username { get; set; }

        public void Apply(IReadModelContext context, IDomainEvent<UserAggregate, UserId, UserUsernameChangedEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;
            Username = domainEvent.AggregateEvent.Username.Value;
        }

        public void Apply(IReadModelContext context, IDomainEvent<UserAggregate, UserId, UserDeletedEvent> domainEvent)
        {
            context.MarkForDeletion();
        }
    }
}
