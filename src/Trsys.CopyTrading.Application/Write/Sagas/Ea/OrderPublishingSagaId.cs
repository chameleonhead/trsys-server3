using EventFlow.Sagas;

namespace Trsys.CopyTrading.Application.Write.Sagas.Ea
{
    public class OrderPublishingSagaId : ISagaId
    {
        public OrderPublishingSagaId(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}