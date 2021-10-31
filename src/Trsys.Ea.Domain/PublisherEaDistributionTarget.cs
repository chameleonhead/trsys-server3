using EventFlow.ValueObjects;
using System.Collections.Generic;
using Trsys.Core;

namespace Trsys.Ea.Domain
{
    public class PublisherEaDistributionTarget : ValueObject
    {
        public PublisherEaDistributionTarget(DistributionGroupId distributionGroupId)
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
