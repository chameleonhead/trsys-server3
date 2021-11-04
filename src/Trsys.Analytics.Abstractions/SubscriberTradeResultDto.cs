namespace Trsys.Analytics.Abstractions
{
    public class SubscriberTradeResultDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public TradeDurationDto? Duration { get; set; }
        public TradeScoreDto? Score { get; set; }
        public decimal? TradeLots { get; set; }
        public TradeProfitDto? Profits { get; set; }
    }
}
