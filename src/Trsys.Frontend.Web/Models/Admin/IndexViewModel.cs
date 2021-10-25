namespace Trsys.Frontend.Web.Models.Admin
{
    public class IndexViewModel
    {
        public string EaSiteUrl { get; set; }
        public UsersViewModel Users { get; set; }
        public PublishersViewModel Publishers { get; set; }
        public SubscribersViewModel Subscribers { get; set; }
        public CopyTradesViewModel CopyTrades { get; set; }
        public DistributionGroupsViewModel DistributionGroups { get; set; }
    }
}
