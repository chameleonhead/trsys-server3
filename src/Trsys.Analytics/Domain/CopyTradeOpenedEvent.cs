using EventFlow.Aggregates;
using Trsys.Core;

namespace Trsys.Analytics.Domain
{
    public class CopyTradeOpenedEvent : AggregateEvent<CopyTradeAggregate, CopyTradeId>
    {
    }
}
