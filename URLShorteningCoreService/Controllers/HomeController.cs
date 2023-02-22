using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using URLShorteningCoreService.Helpers;
using URLShorteningCoreService.Models;
using URLShorteningCoreService.Services;

namespace URLShorteningCoreService.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        private readonly IURLShorteningService _service;

        public HomeController(IURLShorteningService service)
        {
            _service = service;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string originalUrl)
        {
            var shortUrl = new ShortUrlModel
            {
                OriginalUrl = originalUrl
            };

            TryValidateModel(shortUrl);
            if (ModelState.IsValid)
            {
                _service.SaveAsync(shortUrl);

                return (IActionResult)RedirectToAction(actionName: nameof(Show), routeValues: new { id = shortUrl.Id });
            }

            return (IActionResult)View(shortUrl);
        }

        public IActionResult Show(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var shortUrl = _service.GetById(id);
            if (shortUrl == null)
            {
                return NotFound();
            }

            ViewData["Path"] = ShortUrlHelper.GetShortUrl(shortUrl.Id);

            return View(shortUrl);
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

        public IActionResult RedirectTo(string path)
        {
            if (path == null)
            {
                return NotFound();
            }

            var shortUrl = _service.GetByPathAsync(path);
            if (shortUrl == null)
            {
                return NotFound();
            }

            return View("Index",shortUrl.Result.OriginalUrl);
        }
    }
}
