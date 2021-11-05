using System;
using EventFlow.ValueObjects;
using Newtonsoft.Json;

namespace Trsys.Core
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class Currency : SingleValueObject<string>
    {
        public Currency(string value) : base(value)
        {
        }

        public static Currency ValueOf(string value)
        {
            return new Currency(value);
        }

        public Price PriceOf(decimal value)
        {
            return new Price(this, value);
        }
    }
}