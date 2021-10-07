using EventFlow.Sagas;

namespace Trsys.CopyTrading.Application.Write
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