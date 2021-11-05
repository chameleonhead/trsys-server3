using System.Collections.Generic;
using EventFlow.ValueObjects;

namespace Trsys.Core
{
    public class Price : ValueObject
    {
        public Price(Currency currency, decimal value)
        {
            Currency = currency;
            Value = value;
        }

        public Currency Currency { get; }
        public decimal Value { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Currency;
            yield return Value;
        }
    }
}