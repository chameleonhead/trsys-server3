using EventFlow.ValueObjects;

namespace Trsys.CopyTrading.Domain
{
    public class PublisherIdentifier : SingleValueObject<string>
    {
        public PublisherIdentifier(string value) : base(value)
        {
        }
    }
}