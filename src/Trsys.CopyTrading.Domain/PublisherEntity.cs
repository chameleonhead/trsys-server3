using EventFlow.Entities;
using System.Collections.Generic;

namespace Trsys.CopyTrading.Domain
{
    public class PublisherEntity : Entity<PublisherId>
    {
        public PublisherEntity(PublisherId id, DistributionGroupId distributionGroupId, PublisherIdentifier clientKey) : base(id)
        {
            DistributionGroupId = distributionGroupId;
            ClientKey = clientKey; 
        }

        public DistributionGroupId DistributionGroupId { get; private set; }
        public PublisherIdentifier ClientKey { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ClientKey;
        }
    }
}
