using System;
using System.Text.Json.Serialization;
using EventFlow.ValueObjects;

namespace Trsys.BackOffice.Domain
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class Role : SingleValueObject<string>
    {
        public static readonly Role Administrator = new("Administrator");

        public Role(string value) : base(value)
        {
        }

        public static Role Of(string role)
        {
            switch (role)
            {
                case "Administrator":
                    return Administrator;
                default:
                    throw new ArgumentException();
            }
        }
    }
}