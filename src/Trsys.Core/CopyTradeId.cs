using EventFlow.Core;
using EventFlow.ValueObjects;
using Newtonsoft.Json;

namespace Trsys.Core
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class CopyTradeId : Identity<CopyTradeId>
    {
        public CopyTradeId(string value) : base(value)
        {
        }
    }
}