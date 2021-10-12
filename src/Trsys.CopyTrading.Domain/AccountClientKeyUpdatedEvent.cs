using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class AccountClientKeyUpdatedEvent : AggregateEvent<AccountAggregate, AccountId>
    {
        public AccountClientKeyUpdatedEvent(ClientKey clientKey)
        {
            ClientKey = clientKey;
        }

        public ClientKey ClientKey { get; }
    }
}
