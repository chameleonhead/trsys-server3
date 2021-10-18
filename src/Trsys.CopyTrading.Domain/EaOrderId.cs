using EventFlow.Core;

namespace Trsys.CopyTrading.Domain
{
    public class EaOrderId : Identity<EaOrderId>
    {
        public EaOrderId(string value) : base(value)
        {
        }
    }
}
