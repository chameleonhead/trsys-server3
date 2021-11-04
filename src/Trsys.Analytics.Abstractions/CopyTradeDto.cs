using System.Collections.Generic;

namespace Trsys.Analytics.Abstractions
{
    public class CopyTradeDto
    {
        public string Id { get; set; } = "N/A";
        public string Symbol { get; set; } = "N/A";
        public string OrderType { get; set; } = "N/A";
        public TradeDurationDto Duration { get; set; } = new();
        public TradeScoreDto? Score { get; set; }
        public decimal? TotalTradeLots { get; set; }
        public List<TradeProfitDto> TotalProfits { get; set; } = new();

        public PublisherTradeResultDto? PublisherTradeResult { get; set; }
        public List<SubscriberTradeResultDto> SubscriberTradeResult { get; } = new();
    }
}