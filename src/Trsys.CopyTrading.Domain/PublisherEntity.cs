using EventFlow.Entities;

namespace Trsys.CopyTrading.Domain
{
    public class PublisherEntity : Entity<PublisherId>
    {
        public PublisherEntity(PublisherId id) : base(id)
        {
        }
    }
}
