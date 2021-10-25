using System.Collections.Generic;
using Trsys.BackOffice;

namespace Trsys.Frontend.Web.Models.Admin
{
    public class SubscribersViewModel
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int TotalCount { get; set; }
        public List<SubscriberDto> Items { get; set; }
    }
}
