using EventFlow.Aggregates;
using Trsys.Core;

namespace Trsys.Analytics.Domain
{
    public class CopyTradeAggregate : AggregateRoot<CopyTradeAggregate, CopyTradeId>
    {
        public CopyTradeAggregate(CopyTradeId id) : base(id)
        {
        }
    }
}