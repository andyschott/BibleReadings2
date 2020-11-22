using System.Collections.Generic;

namespace BibleReadings2.Models
{
    public class SettingsViewModel
    {
        public TranslationViewModel? EnglishTranslations { get; set; }
        public TranslationViewModel? GermanTranslations { get; set; }
        public IEnumerable<TimeZoneViewModel>? TimeZones { get; set; }
        public string? SelectedTimeZone { get; set; }
    }
}