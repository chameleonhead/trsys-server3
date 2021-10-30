using EventFlow.Aggregates;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Trsys.CopyTrading.Application.Write.Subscribers
{
    public class AllEventBus
    {
        public BlockingCollection<IReadOnlyCollection<IDomainEvent>> Events { get; } = new();
    }
}
