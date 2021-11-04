using EventFlow.Aggregates;
using Trsys.Core;

namespace Trsys.Analytics.Domain
{
    public class CopyTradeClosedEvent : AggregateEvent<CopyTradeAggregate, CopyTradeId>
    {
    }
}