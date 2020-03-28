using System.Collections.Generic;
using System.Linq;
using BibleReadings2.Repository;

namespace BibleReadings2.Helpers
{
    public static class BibleGateway
    {
        private const string BaseUrl = "http://www.biblegateway.com/passage/?search=";

        public static string BuildReadingText(Reading reading)
        {
            if(reading.StartChapter == reading.EndChapter)
            {
                if(reading.StartVerse == reading.EndVerse)
                {
                    return $"{reading.Book} {reading.StartChapter}";
                }
                else
                {
                    return $"{reading.Book} {reading.StartChapter}:{reading.StartVerse} - {reading.EndVerse}";
                }
            }

            if(reading.StartVerse == reading.EndVerse)
            {
                return $"{reading.Book} {reading.StartChapter} - {reading.EndChapter}";
            }

            return $"{reading.Book} {reading.StartChapter}:{reading.StartVerse} - {reading.EndChapter}:{reading.EndVerse}";
        }

        public static string BuildReadingUrl(Reading reading) => BaseUrl + BuildReadingText(reading);

        public static string BuildAllReadingsUrl(IEnumerable<Reading> readings)
        {
            if(!readings.Any())
            {
                return string.Empty;
            }

            var readingTexts = readings.Select(reading => BuildReadingText(reading));
            return BaseUrl + string.Join(';', readingTexts);
        }
    }
}