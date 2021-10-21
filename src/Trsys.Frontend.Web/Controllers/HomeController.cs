using EventFlow.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Application.Read.Models;
using Trsys.Frontend.Web.Models;
using Trsys.Frontend.Web.Models.Home;

namespace Trsys.Frontend.Web.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IQueryProcessor queryProcessor;

        public HomeController(ILogger<HomeController> logger, IQueryProcessor queryProcessor)
        {
            this.logger = logger;
            this.queryProcessor = queryProcessor;
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
            var user = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<LoginReadModel>(vm.Username.ToUpperInvariant()), cancellationToken);
            if (user == null)
            {
                ViewData["ErrorMessage"] = "ユーザー名またはパスワードが違います。";
                return View("Login", vm);
            }
            return Redirect(returnUrl ?? "/");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
