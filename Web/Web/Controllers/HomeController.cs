using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;
using Web.Models;

namespace Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IStringLocalizer<HomeController> _localizer;

		public HomeController(IStringLocalizer<HomeController> localizer,ILogger<HomeController> logger)
		{
			_localizer = localizer;
			_logger = logger;
		}

		public IActionResult Index()
		{
			ViewData["hosgeldiniz"] = _localizer["hosgeldiniz"];
			return View();
		}

		[HttpPost]
		public IActionResult CultureManagement(string culture, string returnUrl)
		{
			Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), new CookieOptions { Expires = DateTimeOffset.Now.AddDays(30) });
			return LocalRedirect(returnUrl);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}