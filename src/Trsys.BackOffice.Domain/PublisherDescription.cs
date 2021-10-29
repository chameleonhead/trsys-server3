using EventFlow.ValueObjects;
using System.Text.Json.Serialization;

namespace Trsys.BackOffice.Domain
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class SubscriberDescription : SingleValueObject<string>
    {
        public SubscriberDescription(string value) : base(value)
        {
        }
    }
}