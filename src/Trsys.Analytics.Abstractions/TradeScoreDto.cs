namespace Trsys.Analytics.Abstractions
{
    public class TradeScoreDto
    {
        public string OrderType { get; set; }
        public decimal? PriceOpened { get; set; }
        public decimal? PriceClosed { get; set; }
        public decimal? PriceChanged => PriceClosed - PriceOpened;
        public bool? IsWin => PriceChanged.HasValue
            ? OrderType == "BUY"
                ? PriceChanged.Value > 0
                : PriceChanged.Value < 0
            : default;
    }
}
