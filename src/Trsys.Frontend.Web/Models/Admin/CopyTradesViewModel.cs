using System.Collections.Generic;
using Trsys.BackOffice;

namespace Trsys.Frontend.Web.Models.Admin
{
    public class CopyTradesViewModel
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int TotalCount { get; set; }
        public List<CopyTradeDto> Items { get; set; }
    }
}
