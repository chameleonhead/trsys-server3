using EventFlow.Core;

namespace Trsys.CopyTrading.Domain
{
    public class SubscriberEaId : Identity<SubscriberEaId>
    {
        public SubscriberEaId(string value) : base(value)
        {
        }
    }
}