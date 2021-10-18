using System.Text.RegularExpressions;

namespace Trsys.CopyTrading
{
    public class OrderTextItem
    {
        public OrderTextItem(int ticketNo, string symbol, OrderType orderType)
        {
            TicketNo = ticketNo;
            Symbol = symbol;
            OrderType = orderType;
        }

        public int TicketNo { get; }
        public string Symbol { get; }
        public OrderType OrderType { get; }

        public static OrderTextItem Parse(string text)
        {
            if (!Regex.IsMatch(text, @"^\d+:[A-Z]+:[01]"))
            {
                throw new OrderTextFormatException();
            }
            var splitted = text.Split(":");
            var ticketNo = splitted[0];
            var symbol = splitted[1];
            var orderType = (OrderType)int.Parse(splitted[2]);

            return new OrderTextItem(int.Parse(ticketNo), symbol, orderType);
        }

        public override string ToString()
        {
            return $"{TicketNo}:{Symbol}:{(int)OrderType}";
        }
    }

    static class DecimalExtension
    {
        public static decimal Normalize(this decimal value)
        {
            return value / 1.000000000000000000000000000000000m;
        }
    }
}
