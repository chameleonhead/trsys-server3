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
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("login")]
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
        public IActionResult ChangePassword([FromQuery] string returnUrl)
        {
            var vm = new ChangePasswordViewModel();
            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
