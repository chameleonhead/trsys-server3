using EventFlow.ValueObjects;
using System.Text.Json.Serialization;

namespace Trsys.BackOffice.Domain
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class SubscriberName : SingleValueObject<string>
    {
        public SubscriberName(string value) : base(value)
        {
        }
    }
}