using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Abstractions;
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
        public IActionResult Index()
        {
            return RedirectToAction("Users");
        }

        [HttpGet("users")]
        public async Task<IActionResult> Users()
        {
            ViewData["SuccessMessage"] = TempData["SuccessMessage"];
            ViewData["ErrorMessage"] = TempData["ErrorMessage"];

            var vm = await GetUsersAsync();
            return View(vm);
        }

        [HttpPost("users/create")]
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
            ViewData["SuccessMessage"] = TempData["SuccessMessage"];
            ViewData["ErrorMessage"] = TempData["ErrorMessage"];

            var vm = await GetDistributionGroupsAsync();
            return View(vm);
        }

        [HttpPost("groups/create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DistributionGroupCreateConfirm([FromForm] DistributionGroupCreateViewModel vm, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return ValidationError(ModelState);
            }

            try
            {
                await distributionGroupService.CreateAsync(vm.Name, cancellationToken);
                return Success("正常に登録されました。");
            }
            catch (InvalidOperationException ex)
            {
                return Fail(ex.Message);
            }
        }

        [HttpPost("groups/{id}/name/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DistributionGroupEditNameConfirm(string id, [FromForm] DistributionGroupEditNameViewModel vm, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return ValidationError(ModelState);
            }

            try
            {
                await distributionGroupService.UpdateNameAsync(id, vm.Name, cancellationToken);
                return Success("正常に登録されました。");
            }
            catch (InvalidOperationException ex)
            {
                return Fail(ex.Message);
            }
        }

        [HttpPost("groups/{id}/delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DistributionGroupDeleteConfirm(string id, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return ValidationError(ModelState);
            }

            try
            {
                await distributionGroupService.DeleteAsync(id, cancellationToken);
                return Success("正常に削除されました。");
            }
            catch (InvalidOperationException ex)
            {
                return Fail(ex.Message);
            }
        }

        [HttpGet("pubs")]
        public async Task<IActionResult> Publishers()
        {
            ViewData["SuccessMessage"] = TempData["SuccessMessage"];
            ViewData["ErrorMessage"] = TempData["ErrorMessage"];

            var vm = await GetPublishersAsync();
            return View(vm);
        }

        [HttpPost("pubs/create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PublisherCreateConfirm([FromForm] PublisherCreateViewModel vm, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return ValidationError(ModelState);
            }

            try
            {
                await publisherService.CreateAsync(vm.Name, vm.Description, cancellationToken);
                return Success("正常に登録されました。");
            }
            catch (InvalidOperationException ex)
            {
                return Fail(ex.Message);
            }
        }

        [HttpPost("pubs/{id}/name/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PublisherEditNameConfirm(string id, [FromForm] PublisherEditNameViewModel vm, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return ValidationError(ModelState);
            }

            try
            {
                await publisherService.UpdateNameAsync(id, vm.Name, cancellationToken);
                return Success("正常に登録されました。");
            }
            catch (InvalidOperationException ex)
            {
                return Fail(ex.Message);
            }
        }

        [HttpPost("pubs/{id}/description/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PublisherEditDescriptionConfirm(string id, [FromForm] PublisherEditDescriptionViewModel vm, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return ValidationError(ModelState);
            }

            try
            {
                await publisherService.UpdateDescriptionAsync(id, vm.Description, cancellationToken);
                return Success("正常に登録されました。");
            }
            catch (InvalidOperationException ex)
            {
                return Fail(ex.Message);
            }
        }

        [HttpPost("pubs/{id}/delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PublisherDeleteConfirm(string id, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return ValidationError(ModelState);
            }

            try
            {
                await publisherService.DeleteAsync(id, cancellationToken);
                return Success("正常に削除されました。");
            }
            catch (InvalidOperationException ex)
            {
                return Fail(ex.Message);
            }
        }

        [HttpGet("subs")]
        public async Task<IActionResult> Subscribers()
        {
            ViewData["SuccessMessage"] = TempData["SuccessMessage"];
            ViewData["ErrorMessage"] = TempData["ErrorMessage"];

            var vm = await GetSubscribersAsync();
            return View(vm);
        }

        [HttpPost("subs/create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubscriberCreateConfirm([FromForm] SubscriberCreateViewModel vm, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return ValidationError(ModelState);
            }

            try
            {
                await subscriberService.CreateAsync(vm.Name, vm.Description, cancellationToken);
                return Success("正常に登録されました。");
            }
            catch (InvalidOperationException ex)
            {
                return Fail(ex.Message);
            }
        }

        [HttpPost("subs/{id}/name/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubscriberEditNameConfirm(string id, [FromForm] SubscriberEditNameViewModel vm, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return ValidationError(ModelState);
            }

            try
            {
                await subscriberService.UpdateNameAsync(id, vm.Name, cancellationToken);
                return Success("正常に登録されました。");
            }
            catch (InvalidOperationException ex)
            {
                return Fail(ex.Message);
            }
        }

        [HttpPost("subs/{id}/description/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubscriberEditDescriptionConfirm(string id, [FromForm] SubscriberEditDescriptionViewModel vm, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return ValidationError(ModelState);
            }

            try
            {
                await subscriberService.UpdateDescriptionAsync(id, vm.Description, cancellationToken);
                return Success("正常に登録されました。");
            }
            catch (InvalidOperationException ex)
            {
                return Fail(ex.Message);
            }
        }

        [HttpPost("subs/{id}/delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubscriberDeleteConfirm(string id, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return ValidationError(ModelState);
            }

            try
            {
                await subscriberService.DeleteAsync(id, cancellationToken);
                return Success("正常に削除されました。");
            }
            catch (InvalidOperationException ex)
            {
                return Fail(ex.Message);
            }
        }

        [HttpGet("trades")]
        public async Task<IActionResult> CopyTrades()
        {
            ViewData["SuccessMessage"] = TempData["SuccessMessage"];
            ViewData["ErrorMessage"] = TempData["ErrorMessage"];

            var vm = await GetCopyTradesAsync();
            return View(vm);
        }

        [HttpPost("trades/open")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CopyTradeOpenConfirm([FromForm] CopyTradeOpenViewModel vm, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return ValidationError(ModelState);
            }

            try
            {
                await copyTradeService.OpenAsync(vm.DistributionGroupId, vm.Symbol, vm.OrderType, cancellationToken);
                return Success("正常に登録されました。");
            }
            catch (InvalidOperationException ex)
            {
                return Fail(ex.Message);
            }
        }

        [HttpPost("trades/{id}/close")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CopyTradeCloseConfirm(string id, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return ValidationError(ModelState);
            }

            try
            {
                await copyTradeService.CloseAsync(id, cancellationToken);
                return Success("正常に登録されました。");
            }
            catch (InvalidOperationException ex)
            {
                return Fail(ex.Message);
            }
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
            var distributionGroups = await distributionGroupService.SearchAsync(page, 0, cancellationToken);
            var result = await copyTradeService.SearchAsync(false, page, perPage, cancellationToken);
            return new CopyTradesViewModel()
            {
                Page = result.Page,
                PerPage = result.PerPage,
                TotalCount = result.TotalCount,
                Items = result.Items,
                DistributionGroups = distributionGroups.Items,
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
