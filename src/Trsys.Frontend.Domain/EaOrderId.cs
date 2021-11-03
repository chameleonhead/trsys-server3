using EventFlow.Core;

namespace Trsys.Frontend.Domain
{
    public class EaOrderId : Identity<EaOrderId>
    {
        public EaOrderId(string value) : base(value)
        {
        }
    }
}
