using EventFlow.ValueObjects;

namespace Trsys.CopyTrading.Domain
{
    public class OrderType : SingleValueObject<string>
    {
        public OrderType(string value) : base(value)
        {
        }

        public static readonly OrderType Buy = new OrderType("BUY");
        public static readonly OrderType Sell = new OrderType("SELL");
    }
}
