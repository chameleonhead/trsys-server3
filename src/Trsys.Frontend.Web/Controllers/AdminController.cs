using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Trsys.BackOffice;
using Trsys.Frontend.Web.Models.Admin;

namespace Trsys.Frontend.Web.Controllers
{
    [Route("admin")]
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IUserService userService;
        private readonly IDistributionGroupService distributionGroupService;
        private readonly IPublisherService publisherService;
        private readonly ISubscriberService subscriberService;
        private readonly ICopyTradeService copyTradeService;

        public AdminController(ILogger<HomeController> logger,
            IUserService userService,
            IDistributionGroupService distributionGroupService,
            IPublisherService publisherService,
            ISubscriberService subscriberService,
            ICopyTradeService copyTradeService)
        {
            this.logger = logger;
            this.userService = userService;
            this.distributionGroupService = distributionGroupService;
            this.publisherService = publisherService;
            this.subscriberService = subscriberService;
            this.copyTradeService = copyTradeService;
        }

        private async Task<UsersViewModel> GetUsersAsync(int page = 1, int perPage = 10)
        {
            var users = await userService.SearchAsync(page, perPage);
            return new UsersViewModel()
            {
                Page = users.Page,
                PerPage = users.PerPage,
                TotalCount = users.TotalCount,
                Items = users.Items,
            };
        }

        private Task<DistributionGroupsViewModel> GetDistributionGroupsAsync(int page = 1, int perPage = 10)
        {
            return Task.FromResult(new DistributionGroupsViewModel());
        }

        private Task<PublishersViewModel> GetPublishersAsync(int page = 1, int perPage = 10)
        {
            return Task.FromResult(new PublishersViewModel());
        }

        private Task<SubscribersViewModel> GetSubscribersAsync(int page = 1, int perPage = 10)
        {
            return Task.FromResult(new SubscribersViewModel());
        }

        private Task<CopyTradesViewModel> GetCopyTradesAsync(int page = 1, int perPage = 10)
        {
            return Task.FromResult(new CopyTradesViewModel());
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var vm = new IndexViewModel();
            vm.Users = await GetUsersAsync();
            vm.DistributionGroups = await GetDistributionGroupsAsync();
            vm.Publishers = await GetPublishersAsync();
            vm.Subscribers = await GetSubscribersAsync();
            vm.CopyTrades = await GetCopyTradesAsync();
            return View(vm);
        }

        [HttpGet("users")]
        public async Task<IActionResult> Users()
        {
            var vm = await GetUsersAsync();
            return PartialView("_Users", vm);
        }

        [HttpGet("groups")]
        public async Task<IActionResult> DistributionGroups()
        {
            var vm = await GetDistributionGroupsAsync();
            return PartialView("_DistributionGroups", vm);
        }

        [HttpGet("pubs")]
        public async Task<IActionResult> Publishers()
        {
            var vm = await GetPublishersAsync();
            return PartialView("_Publishers", vm);
        }

        [HttpGet("subs")]
        public async Task<IActionResult> Subscribers()
        {
            var vm = await GetSubscribersAsync();
            return PartialView("_Subscribers", vm);
        }

        [HttpGet("trades")]
        public async Task<IActionResult> CopyTrades()
        {
            var vm = await GetCopyTradesAsync();
            return PartialView("_CopyTrades", vm);
        }
    }
}
