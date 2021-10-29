using EventFlow.ValueObjects;
using System.Text.Json.Serialization;

namespace Trsys.BackOffice.Domain
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class CopyTradeSymbol : SingleValueObject<string>
    {
        public CopyTradeSymbol(string value) : base(value)
        {
        }
    }
}