using EventFlow.Core;

namespace Trsys.Frontend.Domain
{
    public class SubscriberEaId : Identity<SubscriberEaId>
    {
        public SubscriberEaId(string value) : base(value)
        {
        }
    }
}