using EventFlow.ValueObjects;
using Newtonsoft.Json;
using System;

namespace Trsys.CopyTrading.Domain
{
    [JsonConverter(typeof(OrderTypeConverter))]
    public class OrderType : SingleValueObject<string>
    {
        private OrderType(string value) : base(value)
        {
        }

        public static readonly OrderType Buy = new OrderType("BUY");
        public static readonly OrderType Sell = new OrderType("SELL");

        public static OrderType Of(string orderType)
        {
            switch (orderType)
            {
                case "BUY":
                    return Buy;
                case "SELL":
                    return Sell;
                default:
                    throw new ArgumentException();
            }
        }
    }

    public class OrderTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(OrderType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value is string value)
            {
                return OrderType.Of(value);
            }
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}
