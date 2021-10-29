using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice;
using Trsys.Frontend.Web.Extensions;
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

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            ViewData["SuccessMessage"] = TempData["SuccessMessage"];
            ViewData["ErrorMessage"] = TempData["ErrorMessage"];

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
            ViewData["UsersSuccessMessage"] = TempData["SuccessMessage"];
            ViewData["UsersErrorMessage"] = TempData["ErrorMessage"];

            var vm = await GetUsersAsync();
            return PartialView("_Users", vm);
        }

        [HttpPost("users")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserCreateConfirm([FromForm] UserCreateViewModel vm, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return ValidationError(ModelState);
            }

            try
            {
                await userService.CreateAsync(vm.Username, vm.Password, vm.Nickname, vm.Roles, cancellationToken);
                return Success("正常に登録されました。");
            }
            catch (InvalidOperationException ex)
            {
                return Fail(ex.Message);
            }
        }

        [HttpPost("users/{id}/password/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserEditPasswordConfirm(string id, [FromForm] UserEditPasswordViewModel vm, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return ValidationError(ModelState);
            }

            try
            {
                await userService.UpdatePasswordHashAsync(id, vm.Password, cancellationToken);
                return Success("正常に登録されました。");
            }
            catch (InvalidOperationException ex)
            {
                return Fail(ex.Message);
            }
        }

        [HttpPost("users/{id}/nickname/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserEditNicknameConfirm(string id, [FromForm] UserEditNicknameViewModel vm, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return ValidationError(ModelState);
            }

            try
            {
                await userService.UpdateNicknameAsync(id, vm.Nickname, cancellationToken);
                return Success("正常に登録されました。");
            }
            catch (InvalidOperationException ex)
            {
                return Fail(ex.Message);
            }
        }

        [HttpPost("users/{id}/roles/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserEditRolesConfirm(string id, [FromForm] UserEditRolesViewModel vm, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return ValidationError(ModelState);
            }

            try
            {
                await userService.UpdateRolesAsync(id, vm.Roles, cancellationToken);
                return Success("正常に登録されました。");
            }
            catch (InvalidOperationException ex)
            {
                return Fail(ex.Message);
            }
        }

        [HttpPost("users/{id}/delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserDeleteConfirm(string id, CancellationToken cancellationToken)
        {
            try
            {
                await userService.DeleteAsync(id, cancellationToken);
                return Success("正常に削除されました。");
            }
            catch (InvalidOperationException ex)
            {
                return Fail(ex.Message);
            }
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

        private async Task<UsersViewModel> GetUsersAsync(int page = 1, int perPage = 10, CancellationToken cancellationToken = default)
        {
            var result = await userService.SearchAsync(page, perPage, cancellationToken);
            return new UsersViewModel()
            {
                Page = result.Page,
                PerPage = result.PerPage,
                TotalCount = result.TotalCount,
                Items = result.Items,
            };
        }

        private async Task<DistributionGroupsViewModel> GetDistributionGroupsAsync(int page = 1, int perPage = 10, CancellationToken cancellationToken = default)
        {
            var result = await distributionGroupService.SearchAsync(page, perPage, cancellationToken);
            return new DistributionGroupsViewModel()
            {
                Page = result.Page,
                PerPage = result.PerPage,
                TotalCount = result.TotalCount,
                Items = result.Items,
            };
        }

        private async Task<PublishersViewModel> GetPublishersAsync(int page = 1, int perPage = 10, CancellationToken cancellationToken = default)
        {
            var result = await publisherService.SearchAsync(page, perPage, cancellationToken);
            return new PublishersViewModel()
            {
                Page = result.Page,
                PerPage = result.PerPage,
                TotalCount = result.TotalCount,
                Items = result.Items,
            };
        }

        private async Task<SubscribersViewModel> GetSubscribersAsync(int page = 1, int perPage = 10, CancellationToken cancellationToken = default)
        {
            var result = await subscriberService.SearchAsync(page, perPage, cancellationToken);
            return new SubscribersViewModel()
            {
                Page = result.Page,
                PerPage = result.PerPage,
                TotalCount = result.TotalCount,
                Items = result.Items,
            };
        }

        private async Task<CopyTradesViewModel> GetCopyTradesAsync(int page = 1, int perPage = 10, CancellationToken cancellationToken = default)
        {
            var result = await copyTradeService.SearchAsync(false, page, perPage, cancellationToken);
            return new CopyTradesViewModel()
            {
                Page = result.Page,
                PerPage = result.PerPage,
                TotalCount = result.TotalCount,
                Items = result.Items,
            };
        }

        private IActionResult ValidationError(ModelStateDictionary modelState)
        {
            if (HttpContext.Request.IsAjaxRequest())
            {
                return BadRequest(modelState);
            }
            TempData["ErrorMessage"] = "入力に誤りがあります。";
            return RedirectToAction("Index");
        }

        private IActionResult Success(string message)
        {
            TempData["SuccessMessage"] = message;
            if (HttpContext.Request.IsAjaxRequest())
            {
                return Ok(new { Success = true, Message = message });
            }
            return RedirectToAction("Index");
        }

        private IActionResult Fail(string message)
        {
            if (HttpContext.Request.IsAjaxRequest())
            {
                return BadRequest(new { Message = message });
            }
            TempData["ErrorMessage"] = message;
            return RedirectToAction("Index");
        }
    }
}
