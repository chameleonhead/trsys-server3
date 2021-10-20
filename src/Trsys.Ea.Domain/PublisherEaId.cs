using EventFlow.Core;

namespace Trsys.Ea.Domain
{
    public class PublisherEaId : Identity<PublisherEaId>
    {
        public PublisherEaId(string value) : base(value)
        {
        }
    }
}