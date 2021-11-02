using System.Collections.Generic;

namespace Trsys.CopyTrading.Abstractions
{
    public class CopyTradingTradeOpenedEvent : ICopyTradingEvent
    {
        public CopyTradingTradeOpenedEvent(string copyTradeId, string distributionGroupId, string symbol, string orderType, List<string> subscribers)
        {
            CopyTradeId = copyTradeId;
            DistributionGroupId = distributionGroupId;
            Symbol = symbol;
            OrderType = orderType;
            Subscribers = subscribers;
        }

        public string CopyTradeId { get; }
        public string DistributionGroupId { get; }
        public string Symbol { get; }
        public string OrderType { get; }
        public List<string> Subscribers { get; }
    }
}
