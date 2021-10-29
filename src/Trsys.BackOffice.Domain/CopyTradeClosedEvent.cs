using EventFlow.Aggregates;

namespace Trsys.BackOffice.Domain
{
    public class CopyTradeClosedEvent : AggregateEvent<CopyTradeAggregate, CopyTradeId>
    {
    }
}