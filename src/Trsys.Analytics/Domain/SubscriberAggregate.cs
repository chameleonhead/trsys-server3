using EventFlow.Aggregates;
using Trsys.Core;

namespace Trsys.Analytics.Domain
{
    public class SubscriberAggregate : AggregateRoot<SubscriberAggregate, SubscriberId>
    {
        public SubscriberAggregate(SubscriberId id) : base(id)
        {
        }
    }
}
