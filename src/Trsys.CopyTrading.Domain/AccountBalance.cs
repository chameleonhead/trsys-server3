using EventFlow.ValueObjects;

namespace Trsys.CopyTrading.Domain
{
    public class AccountBalance : SingleValueObject<decimal>
    {
        public AccountBalance(decimal value) : base(value)
        {
        }
    }
}