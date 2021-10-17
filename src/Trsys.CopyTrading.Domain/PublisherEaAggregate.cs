using EventFlow.Aggregates;

namespace Trsys.CopyTrading.Domain
{
    public class PublisherEaAggregate : AggregateRoot<PublisherEaAggregate, PublisherEaId>,
        IEmit<PublisherEaRegisteredEvent>,
        IEmit<PublisherEaOrderTextUpdatedEvent>
    {
        public SecretKey Key { get; private set; }
        public EaOrderText Text { get; private set; }

        public PublisherEaAggregate(PublisherEaId id) : base(id)
        {
        }

        public void Register(SecretKey key, DistributionGroupId distributionGroupId, PublisherId publisherId)
        {
            Emit(new PublisherEaRegisteredEvent(key, distributionGroupId, publisherId));
        }

        public void UpdateOrderText(EaOrderText text)
        {
            if (Text != text)
            {
                Emit(new PublisherEaOrderTextUpdatedEvent(Key, text));
            }
        }

        public void Apply(PublisherEaRegisteredEvent aggregateEvent)
        {
            Key = aggregateEvent.Key;
        }

        public void Apply(PublisherEaOrderTextUpdatedEvent aggregateEvent)
        {
            Text = aggregateEvent.Text;
        }
    }
}
