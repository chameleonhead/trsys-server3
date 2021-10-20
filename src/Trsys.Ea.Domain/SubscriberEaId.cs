using EventFlow.Core;

namespace Trsys.Ea.Domain
{
    public class SubscriberEaId : Identity<SubscriberEaId>
    {
        public SubscriberEaId(string value) : base(value)
        {
        }
    }
}