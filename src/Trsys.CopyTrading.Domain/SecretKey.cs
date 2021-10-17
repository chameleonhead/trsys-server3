using EventFlow.ValueObjects;

namespace Trsys.CopyTrading.Domain
{
    public class SecretKey : SingleValueObject<string>
    {
        public SecretKey(string value) : base(value)
        {
        }
    }
}