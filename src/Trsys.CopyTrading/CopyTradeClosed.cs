using System.Collections.Generic;

namespace Trsys.CopyTrading
{
    public class CopyTradeClosed : ICopyTradingEvent
    {
        public CopyTradeClosed(string copyTradeId, string distributionGroupId, List<string> subscribers)
        {
            CopyTradeId = copyTradeId;
            DistributionGroupId = distributionGroupId;
        }

        public string CopyTradeId { get; }
        public string DistributionGroupId { get; }
        public List<string> Subscribers { get; }
    }
}
