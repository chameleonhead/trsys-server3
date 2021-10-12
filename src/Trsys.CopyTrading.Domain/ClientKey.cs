using EventFlow.ValueObjects;

namespace Trsys.CopyTrading.Domain
{
    public class ClientKey : SingleValueObject<string>
    {
        public ClientKey(string value) : base(value)
        {
        }
    }
}