using EventFlow.ValueObjects;
using System.Text.Json.Serialization;

namespace Trsys.BackOffice.Domain
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class DistributionGroupName : SingleValueObject<string>
    {
        public DistributionGroupName(string value) : base(value)
        {
        }
    }
}