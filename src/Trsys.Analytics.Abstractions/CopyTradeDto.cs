using System.Collections.Generic;

namespace Trsys.Analytics.Abstractions
{
    public class CopyTradeDto
    {
        public string Id { get; set; }
        public string Symbol { get; set; }
        public string OrderType { get; set; }
        public TradeDurationDto Duration { get; set; }
        public TradeScoreDto Score { get; set; }
        public decimal? TotalTradeLots { get; set; }
        public List<TradeProfitDto> TotalProfits { get; set; }

        public PublisherTradeResultDto PublisherTradeResult { get; set; }
        public List<SubscriberTradeResultDto> SubscriberTradeResult { get; set; }
    }
}