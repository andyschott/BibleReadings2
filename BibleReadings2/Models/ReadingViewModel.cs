using BibleReadings2.Helpers;
using BibleReadings2.Repository;

namespace BibleReadings2.Models
{
    public class ReadingViewModel
    {
        public Reading Reading { get; }

        public ReadingViewModel(Reading reading)
        {
            Reading = reading;
        }

        public string ReadingText => BibleGateway.BuildReadingText(Reading);

        public string Url => BibleGateway.BuildReadingUrl(Reading);
    }
}