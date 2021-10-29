namespace Trsys.BackOffice
{
    public class CopyTradeDto
    {
        public string Id { get; set; }
        public string DistributionGroupId { get; internal set; }
        public string Symbol { get; set; }
        public string OrderType { get; set; }
        public bool IsClosed { get; set; }
    }
}
