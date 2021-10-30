using EventFlow.Aggregates;
using Trsys.CopyTrading.Abstractions;

namespace Trsys.Ea.Domain
{
    public class SubscriberEaTradeOrderOpenRequestAppliedEvent : AggregateEvent<SubscriberEaAggregate, SubscriberEaId>
    {
        public SubscriberEaTradeOrderOpenRequestAppliedEvent(SecretKey key, CopyTradeId copyTradeId, EaTicketNumber ticketNumber, ForexTradeSymbol symbol, OrderType orderType)
        {
            Key = key;
            CopyTradeId = copyTradeId;
            TicketNumber = ticketNumber;
            Symbol = symbol;
            OrderType = orderType;
        }

        public SecretKey Key { get; }
        public CopyTradeId CopyTradeId { get; }
        public EaTicketNumber TicketNumber { get; }
        public ForexTradeSymbol Symbol { get; }
        public OrderType OrderType { get; }
    }
}