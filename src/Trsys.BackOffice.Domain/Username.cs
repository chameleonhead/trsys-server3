using System.Text.Json.Serialization;
using EventFlow.ValueObjects;

namespace Trsys.BackOffice.Domain
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class Username : SingleValueObject<string>
    {
        public Username(string value) : base(value)
        {
        }
    }
}