using EventFlow.Sagas;

namespace Trsys.CopyTrading.Application.Write.Sagas
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