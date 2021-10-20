using System.Text.Json.Serialization;
using EventFlow.Core;
using EventFlow.ValueObjects;

namespace Trsys.BackOffice.Domain
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class UserId : Identity<UserId>
    {
        public UserId(string value) : base(value)
        {
        }
    }
}