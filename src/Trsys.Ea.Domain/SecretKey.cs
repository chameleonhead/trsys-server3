using EventFlow.ValueObjects;

namespace Trsys.Ea.Domain
{
    public class SecretKey : SingleValueObject<string>
    {
        public SecretKey(string value) : base(value)
        {
        }
    }
}