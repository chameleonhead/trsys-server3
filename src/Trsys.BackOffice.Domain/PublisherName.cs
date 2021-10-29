using EventFlow.ValueObjects;
using System.Text.Json.Serialization;

namespace Trsys.BackOffice.Domain
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class PublisherName : SingleValueObject<string>
    {
        public PublisherName(string value) : base(value)
        {
        }
    }
}