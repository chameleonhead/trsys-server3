using EventFlow.Aggregates;
using Trsys.Core;

namespace Trsys.Analytics.Domain
{
    public class PublisherAggregate : AggregateRoot<PublisherAggregate, PublisherId>,
        IEmit<PublisherOpenedCopyTradeEvent>,
        IEmit<PublisherClosedCopyTradeEvent>
    {
        public PublisherAggregate(PublisherId id) : base(id)
        {
        }

        public void Apply(PublisherOpenedCopyTradeEvent aggregateEvent)
        {
            throw new System.NotImplementedException();
        }

        public void Apply(PublisherClosedCopyTradeEvent aggregateEvent)
        {
            throw new System.NotImplementedException();
        }
    }
}