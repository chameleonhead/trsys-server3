using EventFlow.ValueObjects;
using System.Text.Json.Serialization;

namespace Trsys.BackOffice.Domain
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class PublisherDescription : SingleValueObject<string>
    {
        public PublisherDescription(string value) : base(value)
        {
        }
    }
}