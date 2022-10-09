using BibleReadings2.Helpers;
using BibleReadings2.Repository;

namespace BibleReadings2.Extensions
{
    public static class ReaderExtensions
    {
        public static void AdjustDate(this Reader reader, string? timeZoneId)
        {
            if (string.IsNullOrWhiteSpace(timeZoneId))
            {
                return;
            }

            var timeZone = Utilities.GetTimeZone(timeZoneId);
            reader.Date = reader.Date.ToOffset(timeZone.BaseUtcOffset);
        }
    }
}