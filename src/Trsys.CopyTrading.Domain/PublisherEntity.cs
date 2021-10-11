using EventFlow.Entities;
using System.Collections.Generic;

namespace Trsys.CopyTrading.Domain
{
    public class PublisherEntity : Entity<PublisherId>
    {
        public PublisherEntity(PublisherId id, PublisherIdentifier clientKey) : base(id)
        {
            ClientKey = clientKey; 
        }

        public PublisherIdentifier ClientKey { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ClientKey;
        }
    }
}
