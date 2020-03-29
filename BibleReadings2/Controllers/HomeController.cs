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
        private readonly ITranslationsRepository _translationsRepo;
        private readonly IReaderRepository _readerRepo;

        public HomeController(ILogger<HomeController> logger, 
            IReadingsRepository readingsRepo,
            ITranslationsRepository translationsRepo,
            IReaderRepository readerRepo)
        {
            _logger = logger;
            _readingsRepo = readingsRepo;
            _translationsRepo = translationsRepo;
            _readerRepo = readerRepo;
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
            var readingsTask = _readingsRepo.GetReadings(date.Month, date.Day);
            var englishTask = _translationsRepo.GetTranslations(Languages.English);
            var germanTask = _translationsRepo.GetTranslations(Languages.German);
            var readerTask = _readerRepo.GetReader();

            HttpContext.Request.Cookies.TryGetValue("english", out var english);
            HttpContext.Request.Cookies.TryGetValue("german", out var german);            

            await Task.WhenAll(readingsTask, englishTask, germanTask, readerTask);

            var model = new ReadingsViewModel
            {
                Date = date,
                Readings = readingsTask.Result.Readings.Select(reading => new ReadingViewModel(reading, english, german)),
                English = english,
                German = german,
                LastReader = readerTask.Result,
            };

            return View(model);
        }
        
        [HttpGet("/settings")]
        public async Task<IActionResult> Settings()
        {
            var englishTask = _translationsRepo.GetTranslations(Languages.English);
            var germanTask = _translationsRepo.GetTranslations(Languages.German);

            await Task.WhenAll(englishTask, germanTask);

            HttpContext.Request.Cookies.TryGetValue("english", out var english);
            HttpContext.Request.Cookies.TryGetValue("german", out var german);            

            var model = new SettingsViewModel
            {
                EnglishTranslations = new TranslationViewModel
                {
                    Language = Languages.English,
                    Translations =  englishTask.Result,
                    SelectedTranslation = english,
                },
                GermanTranslations = new TranslationViewModel
                {
                    Language = Languages.German,
                    Translations = germanTask.Result,
                    SelectedTranslation = german
                },
            };

            return View(model);
        }

        [HttpPost("/settings")]
        public IActionResult SaveSettings(string english, string german)
        {
            HttpContext.Response.Cookies.Append("english", english ?? string.Empty);
            HttpContext.Response.Cookies.Append("german", german ?? string.Empty);

            var url = Url.Action("Index");
            return Redirect(url);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
