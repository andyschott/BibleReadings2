using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BibleReadings2.Models;
using BibleReadings2.Repository;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace BibleReadings2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IReadingsRepository _readingsRepo;

        public HomeController(ILogger<HomeController> logger, IReadingsRepository readingsRepo)
        {
            _logger = logger;
            _readingsRepo = readingsRepo;
        }

        public async Task<IActionResult> Index()
        {
            var today = DateTime.Today;

            var todaysReadings = await _readingsRepo.GetReadings(today.Month, today.Day);

            var model = new ReadingsViewModel
            {
                Date = today,
                Readings = todaysReadings.Readings.Select(reading => new ReadingViewModel(reading)),
            };
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
