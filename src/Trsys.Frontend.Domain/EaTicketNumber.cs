using EventFlow.ValueObjects;

namespace Trsys.Frontend.Domain
{
    public class EaTicketNumber : SingleValueObject<int>
    {
        public EaTicketNumber(int value) : base(value)
        {
        }
    }
}
