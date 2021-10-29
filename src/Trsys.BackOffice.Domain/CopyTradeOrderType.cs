using EventFlow.ValueObjects;
using System.Text.Json.Serialization;

namespace Trsys.BackOffice.Domain
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class CopyTradeOrderType : SingleValueObject<string>
    {
        public CopyTradeOrderType(string value) : base(value)
        {
        }
    }
}