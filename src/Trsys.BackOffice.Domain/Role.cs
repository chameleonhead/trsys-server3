using System.Text.Json.Serialization;
using EventFlow.ValueObjects;

namespace Trsys.BackOffice.Domain
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class Role : SingleValueObject<string>
    {
        public static readonly Role Administrator = new("Administrator");

        public Role(string value) : base(value)
        {
        }
    }
}