using System.Threading.Tasks;
using BibleReadings2.Models;
using BibleReadings2.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BibleReadings2.Controllers
{
    public class SettingsController : Controller
    {
        private readonly ITranslationsRepository _translationsRepo;

        public SettingsController(ITranslationsRepository translationsRepo)
        {
            _translationsRepo = translationsRepo;
        }
        
        public async Task<IActionResult> Index()
        {
            var englishTask = _translationsRepo.GetTranslations(Languages.English);
            var germanTask = _translationsRepo.GetTranslations(Languages.German);

            await Task.WhenAll(englishTask, germanTask);

            var model = new SettingsViewModel
            {
                EnglishTranslations = new TranslationViewModel
                {
                    Language = Languages.English,
                    Translations =  englishTask.Result,
                },
                GermanTranslations = new TranslationViewModel
                {
                    Language = Languages.German,
                    Translations = germanTask.Result,
                },
            };

            return View(model);
        }
    }
}