using EventFlow.ValueObjects;
using Newtonsoft.Json;

namespace Trsys.Analytics.Domain
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class Lot : SingleValueObject<decimal>
    {
        public Lot(decimal value) : base(value)
        {
        }
    }
}