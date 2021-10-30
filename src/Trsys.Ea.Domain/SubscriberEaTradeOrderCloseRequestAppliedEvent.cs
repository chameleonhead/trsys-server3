using EventFlow.Aggregates;
using Trsys.CopyTrading.Abstractions;

namespace Trsys.Ea.Domain
{
    public class SubscriberEaTradeOrderCloseRequestAppliedEvent : AggregateEvent<SubscriberEaAggregate, SubscriberEaId>
    {
        public SubscriberEaTradeOrderCloseRequestAppliedEvent(SecretKey key, CopyTradeId copyTradeId, EaTicketNumber ticketNumber)
        {
            Key = key;
            CopyTradeId = copyTradeId;
            TicketNumber = ticketNumber;
        }

        public SecretKey Key { get; }
        public CopyTradeId CopyTradeId { get; }
        public EaTicketNumber TicketNumber { get; }
    }
}