using EventFlow.Aggregates;
using Trsys.Core;

namespace Trsys.Analytics.Domain
{
    public class PublisherAggregate : AggregateRoot<PublisherAggregate, PublisherId>
    {
        public PublisherAggregate(PublisherId id) : base(id)
        {
        }
    }
}