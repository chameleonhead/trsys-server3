using System.Text.Json.Serialization;
using EventFlow.ValueObjects;

namespace Trsys.BackOffice.Domain
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class HashedPassword : SingleValueObject<string>
    {
        public HashedPassword(string value) : base(value)
        {
        }
    }
}