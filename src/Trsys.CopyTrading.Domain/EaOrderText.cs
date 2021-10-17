using EventFlow.ValueObjects;

namespace Trsys.CopyTrading.Domain
{
    public class EaOrderText : SingleValueObject<string>
    {
        public EaOrderText(string value) : base(value)
        {
        }
    }
}
