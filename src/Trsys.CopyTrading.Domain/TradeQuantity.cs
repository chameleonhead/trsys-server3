using EventFlow.ValueObjects;
using System;

namespace Trsys.CopyTrading.Domain
{
    public enum QuantityType
    {
        Percentage,
        FixedVolume,
    }

    public class TradeQuantity : ValueObject
    {
        public TradeQuantity(QuantityType type, decimal value)
        {
            Type = type;
            Value = value;
        }

        public QuantityType Type { get; }
        public decimal Value { get; }

        public override bool Equals(object obj)
        {
            if (!(obj is TradeQuantity q))
            {
                return false;
            }

            return Type == q.Type && Value == q.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, Value);
        }

        public static TradeQuantity Percentage(decimal value)
        {
            return new TradeQuantity(QuantityType.Percentage, value);
        }

        public static TradeQuantity FixedVolume(decimal value)
        {
            return new TradeQuantity(QuantityType.FixedVolume, value);
        }
    }
}
