using EventFlow.Aggregates;

namespace Trsys.BackOffice.Domain
{
    public class UserDeletedEvent : AggregateEvent<UserAggregate, UserId>
    {
    }
}