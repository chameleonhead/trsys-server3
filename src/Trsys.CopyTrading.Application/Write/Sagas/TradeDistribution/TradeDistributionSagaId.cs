using EventFlow.Sagas;

namespace Trsys.CopyTrading.Application.Write.Sagas.TradeDistribution
{
    public class TradeDistributionSagaId : ISagaId
    {
        public TradeDistributionSagaId(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}