using EventFlow.ValueObjects;

namespace Trsys.CopyTrading.Domain
{
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
