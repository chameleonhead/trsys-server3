using EventFlow.ValueObjects;

namespace Trsys.Ea.Domain
{
    public class EaTicketNumber : SingleValueObject<int>
    {
        public EaTicketNumber(int value) : base(value)
        {
        }
    }
}
