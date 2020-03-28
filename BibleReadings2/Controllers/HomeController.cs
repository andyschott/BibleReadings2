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

        [HttpGet]
        public IActionResult Index()
        {
            var today = DateTime.Today;
            var url = Url.Action("GetReading", new
            {
                year = today.Year,
                month = today.Month,
                day = today.Day
            });

            return Redirect(url);
        }

        [HttpGet("/{year}/{month}/{day}")]
        public async Task<IActionResult> GetReading(int year, int month, int day)
        {
            var date = new DateTime(year, month, day);
            var readings = await _readingsRepo.GetReadings(date.Month, date.Day);

            var model = new ReadingsViewModel
            {
                Date = date,
                Readings = readings.Readings.Select(reading => new ReadingViewModel(reading)),
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
