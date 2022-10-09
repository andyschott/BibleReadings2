using System;
using System.Linq;
using BibleReadings2.Repository;

namespace BibleReadings2.Helpers
{
    public static class Utilities
    {
        public static string BuildReaderDescription(Reader? reader)
        {
            if(reader == null)
            {
                return "No one has read the devotion before.";
            }

            return $"{reader.Name} read the devotion {DateDescription(reader.Date.Date)}.";
        }

        private static string DateDescription(DateTime date)
        {
            if(date == default)
            {
                return "last time";
            }

            var today = DateTime.Today;
            if(date == today)
            {
                return "today";
            }

            var yesterday = today.AddDays(-1);
            if(date == yesterday)
            {
                return "yesterday";
            }

            var lastWeek = today.AddDays(-7);
            if(date >= lastWeek)
            {
                return date.DayOfWeek.ToString();
            }

            return date.ToString("D");
        }

        public static DateTime GetToday(string? timezoneId)
        {
            if (string.IsNullOrEmpty(timezoneId))
            {
                return DateTime.UtcNow;
            }

            var timezone = GetTimeZone(timezoneId);
            
            var today = DateTime.UtcNow;
            return today + timezone.BaseUtcOffset;
        }

        public static TimeZoneInfo GetTimeZone(string id)
        {
            var timeZones = TimeZoneInfo.GetSystemTimeZones();
            var timeZone = timeZones.FirstOrDefault(timeZone => timeZone.Id.Equals(id));
            if(timeZone is null)
            {
                return TimeZoneInfo.Utc;
            }

            return timeZone;
        }
    }
}