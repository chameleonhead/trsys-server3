using EventFlow.Entities;
using System.Collections.Generic;

namespace Trsys.CopyTrading.Domain
{
    public class EaOrderEntity : Entity<EaOrderId>
    {
        public EaOrderEntity(EaOrderId id, EaTicketNumber ticketNo, ForexTradeSymbol symbol, OrderType orderType, List<PublisherEaCopyTradeEntity> targets) : base(id)
        {
            TicketNo = ticketNo;
            Symbol = symbol;
            OrderType = orderType;
            Targets = targets;
        }

        public EaTicketNumber TicketNo { get; }
        public ForexTradeSymbol Symbol { get; }
        public OrderType OrderType { get; }
        public List<PublisherEaCopyTradeEntity> Targets { get; }
    }
}
