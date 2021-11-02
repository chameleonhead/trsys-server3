namespace Trsys.CopyTrading.Abstractions
{
    public class CopyTradingSubscriberRemovedEvent : ICopyTradingEvent
    {
        public CopyTradingSubscriberRemovedEvent(string distributionGroupId, string subscriberId)
        {
            DistributionGroupId = distributionGroupId;
            SubscriberId = subscriberId;
        }

        public string DistributionGroupId { get; }
        public string SubscriberId { get; }
    }
}
