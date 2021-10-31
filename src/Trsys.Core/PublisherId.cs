using EventFlow.Core;
using EventFlow.ValueObjects;
using System.Text.Json.Serialization;

namespace Trsys.Core
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class PublisherId : Identity<PublisherId>
    {
        public PublisherId(string value) : base(value)
        {
        }
    }
}