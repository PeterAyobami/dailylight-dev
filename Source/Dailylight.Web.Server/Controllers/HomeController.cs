using Dailylight.Server.Core;
using Dailylight.Web.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Dailylight.Web.Server.Controllers
{
    /// <summary>
    /// Handles the standard web requests
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// The scoped instance of the <see cref="ApplicationDbContext"/>
        /// </summary>
        protected ApplicationDbContext mContext;

        /// <summary>
        /// Default controller
        /// </summary>
        /// <param name="logger">The logger that logs the app information</param>
        /// <param name="context">The injected context</param>
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            mContext = context;
        }

        /// <summary>
        /// The home page endpoint
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// The method to download the dailylight apk file
        /// </summary>
        /// <returns>The daily light apk file</returns>
        [Route(HttpRoutes.DownloadDLApk)]
        public IActionResult DownloadDailyLightApk() => 
            File("/Assets/Files/Dailylight.apk", "application/vnd.android.package-archive", "Dailylight.apk");


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
