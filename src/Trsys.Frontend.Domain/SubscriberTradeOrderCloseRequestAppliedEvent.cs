using EventFlow.Aggregates;
using Trsys.Core;

namespace Trsys.Frontend.Domain
{
    public class SubscriberTradeOrderCloseRequestAppliedEvent : AggregateEvent<SubscriberAggregate, SubscriberId>
    {
        public SubscriberTradeOrderCloseRequestAppliedEvent(SecretKey key, CopyTradeId copyTradeId, EaTicketNumber ticketNumber)
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