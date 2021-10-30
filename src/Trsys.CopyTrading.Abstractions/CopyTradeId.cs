using EventFlow.Core;
using EventFlow.ValueObjects;
using Newtonsoft.Json;

namespace Trsys.CopyTrading.Abstractions
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class CopyTradeId : Identity<CopyTradeId>
    {
        public CopyTradeId(string value) : base(value)
        {
        }
    }
}