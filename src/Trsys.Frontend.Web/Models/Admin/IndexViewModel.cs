namespace Trsys.Frontend.Web.Models.Admin
{
    public class IndexViewModel
    {
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
        public string EaSiteUrl { get; set; }
        public UsersViewModel Users { get; set; }
        public PublishersViewModel Publishers { get; set; }
        public SubscribersViewModel Subscribers { get; set; }
        public CopyTradesViewModel CopyTrades { get; set; }
    }
}
