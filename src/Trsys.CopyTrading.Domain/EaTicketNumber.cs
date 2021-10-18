using EventFlow.ValueObjects;

namespace Trsys.CopyTrading.Domain
{
    public class EaTicketNumber : SingleValueObject<int>
    {
        public EaTicketNumber(int value) : base(value)
        {
        }
    }
}
