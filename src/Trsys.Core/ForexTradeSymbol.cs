using EventFlow.ValueObjects;
using Newtonsoft.Json;

namespace Trsys.Core
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class ForexTradeSymbol : SingleValueObject<string>
    {
        public ForexTradeSymbol(string value) : base(value)
        {
            Base = Currency.ValueOf(value.Substring(0, 3));
            Quote = Currency.ValueOf(value.Substring(3));
        }

        public Currency Base { get; }
        public Currency Quote { get; }

        public static ForexTradeSymbol ValueOf(string symbol)
        {
            return new ForexTradeSymbol(symbol);
        }
    }
}
