namespace Trsys.BackOffice.Abstractions
{
    public class CopyTradeDto
    {
        public string Id { get; set; }
        public string DistributionGroupId { get; set; }
        public string Symbol { get; set; }
        public string OrderType { get; set; }
        public bool IsClosed { get; set; }
    }
}
