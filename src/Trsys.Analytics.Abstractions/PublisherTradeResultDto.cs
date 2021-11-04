namespace Trsys.Analytics.Abstractions
{
    public class PublisherTradeResultDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public TradeDurationDto? Duration { get; set; }
        public TradeScoreDto? Score { get; set; }
    }
}
