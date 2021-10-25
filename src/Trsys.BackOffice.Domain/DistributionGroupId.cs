using EventFlow.ValueObjects;
using System.Text.Json.Serialization;

namespace Trsys.BackOffice.Domain
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class DistributionGroupId : SingleValueObject<string>
    {
        public DistributionGroupId(string value) : base(value)
        {
        }
    }
}
