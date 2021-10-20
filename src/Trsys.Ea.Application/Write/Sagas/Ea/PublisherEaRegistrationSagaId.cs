using EventFlow.Sagas;

namespace Trsys.Ea.Application.Write.Sagas.Ea
{
    public class PublisherEaRegistrationSagaId : ISagaId
    {
        public PublisherEaRegistrationSagaId(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}