using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DatabaseContext.Models;
using Web.Services;
using Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
  //  [Authorize(Roles ="Admin")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ITransientService _transientService;
        private readonly IScopedService _scopedService;
        private readonly ISingletonService _singletonService;
        public HomeController(ILogger<HomeController> logger,
            ISingletonService singletonService,
            IScopedService scopedService,
            ITransientService transient)
        {
            _singletonService = singletonService;
            _scopedService = scopedService;
            _transientService = transient;
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.Random = _singletonService.GetRandom();
            ViewBag.ScopedRandom = _scopedService.GetRandom();
            ViewBag.TransientRandom = _transientService.GetRandom();
            return View();
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
