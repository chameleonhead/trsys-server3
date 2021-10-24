using EventFlow.Queries;
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
using Trsys.BackOffice.Application.Read.Models;
using Trsys.Frontend.Web.Models;
using Trsys.Frontend.Web.Models.Home;

namespace Trsys.Frontend.Web.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly BackOfficeEventFlowRootResolver resolver;

        public HomeController(ILogger<HomeController> logger, BackOfficeEventFlowRootResolver resolver)
        {
            this.logger = logger;
            this.resolver = resolver;
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
            var queryProcessor = resolver.Resolve<IQueryProcessor>();
            var login = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<LoginReadModel>(vm.Username.ToUpperInvariant()), cancellationToken);
            if (login == null || login.PasswordHash != vm.Password)
            {
                ViewData["ErrorMessage"] = "ユーザー名またはパスワードが違います。";
                return View("Login", vm);
            }
            var user = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<UserReadModel>(login.Id), cancellationToken);
            if (user == null)
            {
                ViewData["ErrorMessage"] = "ユーザー名またはパスワードが違います。";
                return View("Login", vm);
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("Nickname", user.Nickname),
                new Claim(ClaimTypes.Role, user.Role),
            };

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
