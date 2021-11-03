using EventFlow.ValueObjects;

namespace Trsys.Frontend.Domain
{
    public class SecretKey : SingleValueObject<string>
    {
        public SecretKey(string value) : base(value)
        {
        }
    }
}