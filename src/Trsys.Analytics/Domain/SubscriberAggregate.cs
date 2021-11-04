using EventFlow.Aggregates;
using Trsys.Core;

namespace Trsys.Analytics.Domain
{
    public class SubscriberAggregate : AggregateRoot<SubscriberAggregate, SubscriberId>,
        IEmit<SubscriberOpenedCopyTradeEvent>,
        IEmit<SubscriberClosedCopyTradeEvent>
    {
        public SubscriberAggregate(SubscriberId id) : base(id)
        {
        }

        public void Apply(SubscriberOpenedCopyTradeEvent aggregateEvent)
        {
            throw new System.NotImplementedException();
        }

        public void Apply(SubscriberClosedCopyTradeEvent aggregateEvent)
        {
            throw new System.NotImplementedException();
        }
    }
}
