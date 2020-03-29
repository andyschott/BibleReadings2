using System.Collections.Generic;
using System.Linq;
using BibleReadings2.Repository;

namespace BibleReadings2.Models
{
    public class TranslationViewModel
    {
        public Languages Language { get; set; }

        private IEnumerable<Translation> _translations = Enumerable.Empty<Translation>();
        public IEnumerable<Translation> Translations
        {
            get { return _translations; }
            set
            {
                _translations = value;
                SelectedTranslation = _translations?.FirstOrDefault()?.ShortName;
            }
        }

        public string? SelectedTranslation { get; set; }
    }
}