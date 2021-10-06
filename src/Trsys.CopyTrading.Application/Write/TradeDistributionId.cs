using EventFlow.Core;
using EventFlow.Sagas;

namespace Trsys.CopyTrading.Application.Write
{
    public class TradeDistributionId : Identity<TradeDistributionId>, ISagaId
    {
        public TradeDistributionId(string value) : base(value)
        {
        }
    }
}