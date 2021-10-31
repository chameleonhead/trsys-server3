using EventFlow.ValueObjects;
using Newtonsoft.Json;

namespace Trsys.Core
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class ForexTradeSymbol : SingleValueObject<string>
    {
        public ForexTradeSymbol(string value) : base(value)
        {
        }

        public static ForexTradeSymbol ValueOf(string symbol)
        {
            return new ForexTradeSymbol(symbol);
        }
    }
}
