namespace Trsys.CopyTrading.Abstractions
{
    public class CopyTradingSubscriberAddedEvent : ICopyTradingEvent
    {
        public CopyTradingSubscriberAddedEvent(string distirbutionGroupId, string subscriberId)
        {
            DistributionGroupId = distirbutionGroupId;
            SubscriberId = subscriberId;
        }

        public string DistributionGroupId { get; }
        public string SubscriberId { get; }
    }
}
