using BibleReadings2.Helpers;
using BibleReadings2.Repository;

namespace BibleReadings2.Models
{
    public class ReadingViewModel
    {
        public Reading Reading { get; }
        private readonly string? _english;
        private readonly string? _german;

        public ReadingViewModel(Reading reading, string? english, string? german)
        {
            Reading = reading;
            _english = english;
            _german = german;
        }

        public string ReadingText => BibleGateway.BuildReadingText(Reading);

        public string Url => BibleGateway.BuildReadingUrl(Reading, _english, _german);
    }
}