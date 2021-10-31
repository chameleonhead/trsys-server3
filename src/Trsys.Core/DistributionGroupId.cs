using EventFlow.Core;
using EventFlow.ValueObjects;
using Newtonsoft.Json;

namespace Trsys.Core
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class DistributionGroupId : Identity<DistributionGroupId>
    {
        public DistributionGroupId(string value) : base(value)
        {
        }
    }
}