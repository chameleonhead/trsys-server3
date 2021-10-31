using System.Collections.Generic;

namespace Trsys.CopyTrading.Abstractions
{
    public class CopyTradeClosed : ICopyTradingEvent
    {
        public CopyTradeClosed(string copyTradeId, List<string> subscribers)
        {
            CopyTradeId = copyTradeId;
            Subscribers = subscribers;
        }

        public string CopyTradeId { get; }
        public List<string> Subscribers { get; }
    }
}
