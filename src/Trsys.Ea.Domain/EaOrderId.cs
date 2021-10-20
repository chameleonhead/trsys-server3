using EventFlow.Core;

namespace Trsys.Ea.Domain
{
    public class EaOrderId : Identity<EaOrderId>
    {
        public EaOrderId(string value) : base(value)
        {
        }
    }
}
