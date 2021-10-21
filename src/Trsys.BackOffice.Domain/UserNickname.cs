using System.Text.Json.Serialization;
using EventFlow.ValueObjects;

namespace Trsys.BackOffice.Domain
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class UserNickname : SingleValueObject<string>
    {
        public UserNickname(string value) : base(value)
        {
        }
    }
}