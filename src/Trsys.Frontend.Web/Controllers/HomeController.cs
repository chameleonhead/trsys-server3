using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice;
using Trsys.Frontend.Web.Models;
using Trsys.Frontend.Web.Models.Home;

namespace Trsys.Frontend.Web.Controllers
{
    [Route("")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IUserService service;

        public HomeController(ILogger<HomeController> logger, IUserService service)
        {
            this.logger = logger;
            this.service = service;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Admin");
        }

        [HttpGet("login")]
        [AllowAnonymous]
        public IActionResult Login([FromQuery] string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect(returnUrl ?? "/");
            }

            var vm = new LoginViewModel()
            {
                ReturnUrl = returnUrl
            };
            return View(vm);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginConfirm([FromQuery] string returnUrl, [FromForm] LoginViewModel vm, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", vm);
            }
            var user = await service.FindUserByUsernameAsync(vm.Username, cancellationToken);
            if (user == null || user.PasswordHash != vm.Password)
            {
                ViewData["ErrorMessage"] = "ユーザー名またはパスワードが違います。";
                return View("Login", vm);
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("Nickname", user.Nickname),
            };
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                IssuedUtc = DateTimeOffset.Now,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return Redirect(returnUrl ?? "/");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }

        [HttpGet("changePassword")]
        public IActionResult ChangePassword()
        {
            ViewData["SuccessMessage"] = TempData["SuccessMessage"];
            var vm = new ChangePasswordViewModel();
            return View(vm);
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePasswordConfirm([FromForm] ChangePasswordViewModel vm, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View("ChangePassword", vm);
            }
            if (vm.NewPassword != vm.NewPasswordConfirm)
            {
                ModelState.AddModelError("NewPasswordConfirm", "確認用パスワードが一致しません。");
                return View("ChangePassword", vm);
            }
            try
            {
                await service.ChangePasswordAsync(User.FindFirstValue(ClaimTypes.NameIdentifier), vm.NewPassword, cancellationToken);
                TempData["SuccessMessage"] = "更新しました。";
                return RedirectToAction("ChangePassword");
            }
            catch
            {
                ViewData["ErrorMessage"] = "更新に失敗しました。";
                return View("ChangePassword", vm);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [AllowAnonymous]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
