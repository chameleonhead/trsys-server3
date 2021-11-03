using EventFlow.ValueObjects;
using System.Collections.Generic;
using Trsys.Core;

namespace Trsys.Frontend.Domain
{
    public class PublisherDistributionTarget : ValueObject
    {
        public PublisherDistributionTarget(DistributionGroupId distributionGroupId)
        {
            DistributionGroupId = distributionGroupId;
        }

        public DistributionGroupId DistributionGroupId { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return DistributionGroupId;
        }
    }
}
