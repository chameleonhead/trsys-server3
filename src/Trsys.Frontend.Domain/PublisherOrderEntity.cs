using EventFlow.Entities;
using System.Collections.Generic;
using Trsys.Core;

namespace Trsys.Frontend.Domain
{
    public class PublisherOrderEntity : Entity<EaOrderId>
    {
        public PublisherOrderEntity(EaOrderId id, EaTicketNumber ticketNo, ForexTradeSymbol symbol, OrderType orderType, List<PublisherCopyTradeEntity> targets) : base(id)
        {
            TicketNo = ticketNo;
            Symbol = symbol;
            OrderType = orderType;
            Targets = targets;
        }

        public EaTicketNumber TicketNo { get; }
        public ForexTradeSymbol Symbol { get; }
        public OrderType OrderType { get; }
        public List<PublisherCopyTradeEntity> Targets { get; }
    }
}
