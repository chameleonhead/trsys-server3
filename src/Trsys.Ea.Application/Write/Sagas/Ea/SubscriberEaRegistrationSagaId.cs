using EventFlow.Sagas;

namespace Trsys.Ea.Application.Write.Sagas.Ea
{
    public class SubscriberEaRegistrationSagaId : ISagaId
    {
        public SubscriberEaRegistrationSagaId(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}