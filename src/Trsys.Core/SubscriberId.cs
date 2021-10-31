using EventFlow.Core;
using EventFlow.ValueObjects;
using System.Text.Json.Serialization;

namespace Trsys.Core
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class SubscriberId : Identity<SubscriberId>
    {
        public SubscriberId(string value) : base(value)
        {
        }
    }
}