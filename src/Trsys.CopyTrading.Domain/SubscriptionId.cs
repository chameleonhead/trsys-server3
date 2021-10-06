using EventFlow.Core;

namespace Trsys.CopyTrading.Domain
{
    public class SubscriptionId : Identity<SubscriptionId>
    {
        public SubscriptionId(string value) : base(value)
        {
        }
    }
}