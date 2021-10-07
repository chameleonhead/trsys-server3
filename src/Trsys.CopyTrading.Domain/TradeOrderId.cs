using EventFlow.Core;

namespace Trsys.CopyTrading.Domain
{
    public class TradeOrderId : Identity<TradeOrderId>
    {
        public TradeOrderId(string value) : base(value)
        {
        }
    }
}