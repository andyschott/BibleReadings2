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

        public static string BuildReadingUrl(Reading reading, string? english, string? german)
        {
            return BuildReadingUrl(BuildReadingText(reading), english, german);
        }

        public static string BuildReadingUrl(string readingText, string? english, string? german)
        {
            return BaseUrl + readingText + GetTranslationQueryString(english, german);
        }

        public static string BuildAllReadingsUrl(IEnumerable<Reading> readings, string? english, string? german)
        {
            if(!readings.Any())
            {
                return string.Empty;
            }

            var readingTexts = readings.Select(reading => BuildReadingText(reading));
            return BaseUrl + string.Join(';', readingTexts) + GetTranslationQueryString(english, german);
        }

        private static string GetTranslationQueryString(string? english, string? german)
        {
            var translations = new[] { english, german }.Where(translation => !string.IsNullOrEmpty(translation));
            if(!translations.Any())
            {
                return string.Empty;
            }

            return $"&version={string.Join(';', translations)}";
        }
    }
}