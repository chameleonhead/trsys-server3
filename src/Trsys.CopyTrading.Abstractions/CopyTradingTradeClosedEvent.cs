using System.Collections.Generic;

namespace Trsys.CopyTrading.Abstractions
{
    public class CopyTradingTradeClosedEvent : ICopyTradingEvent
    {
        public CopyTradingTradeClosedEvent(string copyTradeId, List<string> subscribers)
        {
            CopyTradeId = copyTradeId;
            Subscribers = subscribers;
        }

        public string CopyTradeId { get; }
        public List<string> Subscribers { get; }
    }
}
