using EventFlow.ValueObjects;
using System.Text.Json.Serialization;

namespace Trsys.BackOffice.Domain
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class DistributionGroupDisplayName : SingleValueObject<string>
    {
        public DistributionGroupDisplayName(string value) : base(value)
        {
        }
    }
}