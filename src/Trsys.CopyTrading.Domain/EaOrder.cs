using EventFlow.ValueObjects;
using System.Collections.Generic;

namespace Trsys.CopyTrading.Domain
{
    public class EaOrder: ValueObject
    {
        public EaOrder(EaTicketNumber ticketNo, ForexTradeSymbol symbol, OrderType orderType)
        {
            TicketNo = ticketNo;
            Symbol = symbol;
            OrderType = orderType;
        }

        public EaTicketNumber TicketNo { get; }
        public ForexTradeSymbol Symbol { get; }
        public OrderType OrderType { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return TicketNo;
            yield return Symbol;
            yield return OrderType;
        }
    }
}