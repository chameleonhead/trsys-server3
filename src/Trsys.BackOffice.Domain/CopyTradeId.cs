using EventFlow.Core;
using EventFlow.ValueObjects;
using System.Text.Json.Serialization;

namespace Trsys.BackOffice.Domain
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class CopyTradeId : Identity<SubscriberId>
    {
        public CopyTradeId(string value) : base(value)
        {
        }
    }
}