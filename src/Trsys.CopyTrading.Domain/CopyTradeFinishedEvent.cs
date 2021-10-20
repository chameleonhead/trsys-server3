using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class CopyTradeFinishedEvent : AggregateEvent<CopyTradeAggregate, CopyTradeId>
    {
    }
}