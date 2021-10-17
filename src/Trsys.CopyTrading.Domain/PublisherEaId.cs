using EventFlow.Core;

namespace Trsys.CopyTrading.Domain
{
    public class PublisherEaId : Identity<PublisherEaId>
    {
        public PublisherEaId(string value) : base(value)
        {
        }
    }
}