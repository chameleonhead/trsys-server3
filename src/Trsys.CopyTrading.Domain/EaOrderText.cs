using EventFlow.ValueObjects;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Trsys.CopyTrading.Domain
{
    public class EaOrderText : SingleValueObject<string>
    {
        private IEnumerable<EaOrder> _orders;

        public EaOrderText(string value) : base(value)
        {
        }

        public IEnumerable<EaOrder> ToOrders()
        {
            if (_orders != null)
            {
                return _orders;
            }
            var orders = new List<EaOrder>();
            if (!string.IsNullOrEmpty(Value))
            {
                foreach (var item in Value.Split("@"))
                {
                    if (!Regex.IsMatch(item, @"^\d+:[A-Z]+:[01]($|:.*)"))
                    {
                        throw new EaOrderTextFormatException();
                    }
                    var splitted = item.Split(":");
                    var ticketNo = new EaTicketNumber(int.Parse(splitted[0]));
                    var symbol = new ForexTradeSymbol(splitted[1]);
                    var orderType = splitted[2] == "0" ? OrderType.Buy : OrderType.Sell;

                    orders.Add(new EaOrder(ticketNo, symbol, orderType));
                }
            }
            _orders = orders;
            return orders;
        }

        public static EaOrderText FromOrders(IEnumerable<EaOrder> orders)
        {
            if (!orders.Any())
            {
                return new EaOrderText("");
            }
            return new EaOrderText(string.Join("@", orders.Select(e => $"{e.TicketNo.Value}:{e.Symbol.Value}:{(e.OrderType == OrderType.Buy ? "0" : "1")}")));
        }
    }
}
